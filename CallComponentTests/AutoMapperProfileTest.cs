using System.Collections.Immutable;
using AutoMapper;
using CallComponent;
using Core;
using Database;
using Database.DbModels;

namespace CallComponentTests
{
    [TestClass]
    public sealed class AutoMapperProfileTest
    {
        private static IMapper mapper = null!;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            mapper = new Mapper(configuration);
        }

        [TestMethod]
        public void MapCallIntoDbCall()
        {
            var call = new Call(
                new CallId(1),
                EmotionalTone.Neutral,
                new Transcription("transcription"),
                ImmutableHashSet.Create(new Person("person")),
                ImmutableHashSet.Create(new Location("location")),
                ImmutableList.Create(new Topic(
                    new TopicId(1),
                    new Title("title"),
                    ImmutableHashSet.Create(new Point("point")),
                    new CallId(1)
                )),
                Status.Processing
            );
            var dbCall = mapper.Map<DbCall>(call);
            Assert.AreEqual(1, dbCall.Id);
            Assert.AreEqual(EmotionalTone.Neutral, dbCall.Tone);
            Assert.AreEqual("transcription", dbCall.Transcription);
            Assert.AreEqual(1, dbCall.People.Count);
            Assert.AreEqual("person", dbCall.People.First());
            Assert.AreEqual(1, dbCall.Locations.Count);
            Assert.AreEqual("location", dbCall.Locations.First());
            Assert.AreEqual(1, dbCall.Topics.Count);
            Assert.AreEqual(1, dbCall.Topics.First().Id);
            Assert.AreEqual("title", dbCall.Topics.First().Title);
            Assert.AreEqual(1, dbCall.Topics.First().Points.Count);
            Assert.AreEqual("point", dbCall.Topics.First().Points.First());
            Assert.AreEqual(1, dbCall.Topics.First().CallId);
            Assert.AreEqual(Status.Processing, dbCall.Status);

        }

        [TestMethod]
        public void MapDbCallIntoCall()
        {
            var dbCall = new DbCall
            {
                Id = 1,
                Tone = EmotionalTone.Neutral,
                Locations = ["location"],
                People = ["person"],
                Status = Status.Completed,
                Topics = new List<DbTopic>
                {
                    new DbTopic
                    {
                        Id = 1,
                        Title = "title",
                        Points = ["point"],
                        CallId = 1
                    }
                },
                Transcription = "transcription"
            };
            var call = mapper.Map<Call>(dbCall);
            Assert.AreEqual(1, call.Id.Value);
            Assert.AreEqual(EmotionalTone.Neutral, call.Tone);
            Assert.AreEqual("transcription", call.Transcription.Text);
            Assert.AreEqual(1, call.People.Count);
            Assert.AreEqual("person", call.People.First().Name);
            Assert.AreEqual(1, call.Locations.Count);
            Assert.AreEqual("location", call.Locations.First().Address);
            Assert.AreEqual(1, call.Topics.Count);
            Assert.AreEqual(1, call.Topics[0].Id.Value);
            Assert.AreEqual("title", call.Topics[0].Title.Value);
            Assert.AreEqual(1, call.Topics[0].Points.Count);
            Assert.AreEqual("point", call.Topics[0].Points.First().Value);
            Assert.AreEqual(Status.Completed, call.Status);



        }
    }
}
