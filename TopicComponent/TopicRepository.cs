using AutoMapper;
using Core;
using Core.Exceptions;
using Database;
using Database.DbModels;
using Microsoft.EntityFrameworkCore;

namespace TopicComponent;

internal class TopicRepository(AppDbContext context, IMapper mapper) : ITopicRepository
{
    public async Task<Topic> CreateTopicAsync(Topic topic)
    {
        topic = topic with { Id = TopicId.Default };
        var dbTopic = mapper.Map<DbTopic>(topic);
        await context.Topics.AddAsync(dbTopic);
        await context.SaveChangesAsync();
        context.Entry(dbTopic).State = EntityState.Detached;
        return mapper.Map<Topic>(dbTopic);
    }

    public async Task<Topic> DeleteTopicAsync(TopicId topicId)
    {
        var dbTopic = await context.Topics.FindAsync(topicId.Value);
        if (dbTopic is null)
        {
            throw new EntityNotExistException(nameof(Topic), topicId);
        }
        context.Topics.Remove(dbTopic);
        await context.SaveChangesAsync();
        return mapper.Map<Topic>(dbTopic);
    }

    public async Task<IEnumerable<Topic>> GetCategoriesAsync()
    {
        var dbCategories = await context.Topics.ToListAsync();
        return dbCategories.Select(mapper.Map<Topic>);
    }

    public async Task<Topic> GetTopicAsync(TopicId topicId)
    {
        var dbTopic = await context.Topics.FindAsync(topicId.Value);
        if (dbTopic is null)
        {
            throw new EntityNotExistException(nameof(Topic), topicId);
        }
        return mapper.Map<Topic>(dbTopic);
    }

    public async Task<Topic> UpdateTopicAsync(Topic topic)
    {
        var dbTopic = mapper.Map<DbTopic>(topic);
        var foundTopic = await context.Topics.FindAsync(topic.Id.Value);
        if (foundTopic is null)
        {
            throw new EntityNotExistException(nameof(Topic), topic.Id);
        }
        context.Entry(foundTopic).CurrentValues.SetValues(dbTopic);
        await context.SaveChangesAsync();
        context.Entry(dbTopic).State = EntityState.Detached;
        return mapper.Map<Topic>(dbTopic);
    }
}