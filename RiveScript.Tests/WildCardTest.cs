﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiveScript.Tests
{
    [TestClass]
    public class WildCardTest
    {
        [TestMethod]
        public void Capture_WildCard_Single_Word_End_Letters_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ my name is _",
                              "- Hello, <star>."});

            rs.sortReplies();

            var reply = rs.reply("default", " my name is Bob");

            Assert.AreEqual("Hello, bob.", reply);
        }

        [TestMethod]
        public void Capture_WildCard_Single_Words_Middle_Letters_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ my name is _ for you",
                              "- Hello <star>"});

            rs.sortReplies();

            var reply = rs.reply("default", " my name is Bob for you");

            Assert.AreEqual("Hello bob", reply);
        }

        [TestMethod]
        public void Capture_WildCard_Single_Words_Start_Letters_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ _ is my name",
                              "- Hello <star>"});

            rs.sortReplies();

            var reply = rs.reply("default", " Bob is my name");

            Assert.AreEqual("Hello bob", reply);
        }

        [TestMethod]
        public void Capture_WildCard_Multiple_Words_End_Letters_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ my name is _",
                              "- Hello, <star>."});

            rs.sortReplies();

            var reply = rs.reply("default", " my name is Bob Lee");

            Assert.AreEqual("Hello, bob lee.", reply);
        }

        [TestMethod]
        public void Capture_WildCard_Multiple_Words_Middle_Letters_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ my name is _ for you",
                              "- Hello <star>"});

            rs.sortReplies();

            var reply = rs.reply("default", " my name is Bob Lee for you");

            Assert.AreEqual("Hello bob lee", reply);
        }

        [TestMethod]
        public void Capture_WildCard_Multiple_Words_Start_Letters_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ _ is my name",
                              "- Hello <star>"});

            rs.sortReplies();

            var reply = rs.reply("default", " Bob Lee is my name");

            Assert.AreEqual("Hello bob lee", reply);
        }

        [TestMethod]
        public void WildCard_LetterOnly_Should_Ignore_Single_With_Numbers()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ my name is _",
                              "- Hello, <star>."});

            rs.sortReplies();

            var reply = rs.reply("default", " my name is 123");

            Assert.AreEqual(TestHelper.ErrorNoReplyMatched, reply);
        }

        [TestMethod]
        public void WildCard_LetterOnly_Should_Ignore_Multiple_With_Numbers()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ my name is _",
                              "- Hello, <star>."});

            rs.sortReplies();

            var reply = rs.reply("default", " my name is Bob 123");

            Assert.AreEqual(TestHelper.ErrorNoReplyMatched, reply);
        }

        [TestMethod]
        public void Capture_WildCard_Start_Numbers_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ # is my age",
                              "- Your old is <star>"});

            rs.sortReplies();

            var reply = rs.reply("default", "  27 is my age");

            Assert.AreEqual("Your old is 27", reply);
        }

        [TestMethod]
        public void Capture_WildCard_End_Numbers_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ my age is #",
                              "- Your old is <star>"});

            rs.sortReplies();

            var reply = rs.reply("default", "my age is 27");

            Assert.AreEqual("Your old is 27", reply);
        }

        [TestMethod]
        public void Capture_WildCard_Middle_Numbers_Only()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ is # my age",
                              "- Your old is <star>"});

            rs.sortReplies();

            var reply = rs.reply("default", "Is 27 my age");

            Assert.AreEqual("Your old is 27", reply);
        }

        [TestMethod]
        public void Multiple_WildCard_Capture()
        {
            var rs = new RiveScript(true);

            rs.stream(new[] { "+ _ told me to say *",
                              "- So did you say \"<star2>\" because \"<star1>\" told you to?"});

            rs.sortReplies();

            var reply = rs.reply("default", "Bob told me to say hello man");

            Assert.AreEqual("So did you say \"hello man\" because \"bob\" told you to?", reply);
        }
    }
}