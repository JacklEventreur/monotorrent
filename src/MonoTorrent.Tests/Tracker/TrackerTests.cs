using System;
using NUnit.Framework;
using MonoTorrent.Client.Tracker;
using System.Threading;
using MonoTorrent.Common;

namespace MonoTorrent.Tracker
{
    [TestFixture]
    public class TrackerTests
    {
        //static void Main(string[] args)
        //{
        //    TrackerTests t = new TrackerTests();
        //    t.FixtureSetup();
        //    t.Setup();
        //    t.MultipleAnnounce();
        //    t.FixtureTeardown();
        //}
        Uri uri = new Uri("http://127.0.0.1:23456/");
        Listeners.HttpListener listener;
        Tracker server;
        //MonoTorrent.Client.Tracker.HTTPTracker tracker;
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            listener = new Listeners.HttpListener(uri.OriginalString);
            listener.Start();
            server = new Tracker();
            server.RegisterListener(listener);
            listener.Start();
        }

        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
            listener.Stop();
            server.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            //tracker = new MonoTorrent.Client.Tracker.HTTPTracker(uri);
        }

        [Test]
        public void MultipleAnnounce()
        {
            int announceCount = 0;
            Random r = new Random();
            ManualResetEvent handle = new ManualResetEvent(false);

            for (int i=0; i < 20; i++)
            {
                InfoHash infoHash = new InfoHash(new byte[20]);
                r.NextBytes(infoHash.Hash);
                TrackerTier tier = new TrackerTier(new string[] { uri.ToString() });
                tier.Trackers[0].AnnounceComplete += delegate {
                    if (++announceCount == 20)
                        handle.Set();
                };
                TrackerConnectionID id = new TrackerConnectionID(tier.Trackers[0], false, TorrentEvent.Started, new ManualResetEvent(false));
                Client.Tracker.AnnounceParameters parameters;
                parameters = new Client.Tracker.AnnounceParameters(0, 0, 0, TorrentEvent.Started,
                                                                       infoHash, false, new string('1', 20), "", 1411);
                tier.Trackers[0].Announce(parameters, id);
            }

            Assert.IsTrue(handle.WaitOne(5000, true), "Some of the responses weren't received");
        }
    }
}
