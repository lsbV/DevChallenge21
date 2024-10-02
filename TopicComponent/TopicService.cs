using Core;

namespace TopicComponent;

internal class TopicService(ITopicRepository repository) : ITopicService
{
    public async Task<Topic> CreateTopicAsync(Topic topic)
    {
        return await repository.CreateTopicAsync(topic);
    }

    public async Task<Topic> DeleteTopicAsync(TopicId topicId)
    {
        return await repository.DeleteTopicAsync(topicId);
    }

    public async Task<IEnumerable<Topic>> GetCategoriesAsync()
    {
        return await repository.GetCategoriesAsync();
    }

    public Task<Topic> GetTopicAsync(TopicId topicId)
    {
        return repository.GetTopicAsync(topicId);
    }

    public Task<Topic> UpdateTopicAsync(Topic topic)
    {
        return repository.UpdateTopicAsync(topic);
    }
}