using System.Collections.Immutable;
using AutoMapper;
using CallComponent;
using Core;
using Database;
using Microsoft.EntityFrameworkCore;

namespace CallComponentTests;

[TestClass]
public class CallRepositoryTest
{
    private static IMapper _mapper;
    private AppDbContext _context;
    private CallRepository _repository;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();

    }

    [TestInitialize]
    public void TestInitialize()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AppDbContext(options);
        _context.Database.EnsureCreated();
        _repository = new CallRepository(_context, _mapper);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public async Task SaveCallAsync_CallSaved()
    {
        var call = Call.Empty;
        var callId = await _repository.SaveCallAsync(call);
        var dbCall = await _context.Calls.FindAsync(callId.Value);
        Assert.IsNotNull(dbCall);
    }

    [TestMethod]
    public async Task GetCallByIdAsync_CallReturned()
    {
        var call = Call.Empty with { Tone = EmotionalTone.Angry };
        var callId = await _repository.SaveCallAsync(call);
        var result = await _repository.GetCallByIdAsync(callId);
        Assert.AreEqual(call.Tone, result.Tone);
    }

    [TestMethod]
    public async Task UpdateCallAsync_CallUpdated()
    {
        var call = Call.Empty;
        var callId = await _repository.SaveCallAsync(call);
        var updatedCall = call with { Status = Status.Completed, Id = callId, Topics = ImmutableList.Create<Topic>(new Topic(TopicId.Default, new Title("Title"), ImmutableHashSet.Create<Point>(new Point("Point")), callId)) };
        await _repository.UpdateCallAsync(updatedCall);
        var dbCall = await _context.Calls.FindAsync(callId.Value);
        Assert.AreEqual(Status.Completed, dbCall.Status);
    }


}