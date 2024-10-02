using Core;

namespace TopicComponent;

internal interface ITopicRepository
{
    public Task<Topic> CreateTopicAsync(Topic topic);
    public Task<Topic> DeleteTopicAsync(TopicId topicId);
    public Task<IEnumerable<Topic>> GetCategoriesAsync();
    public Task<Topic> GetTopicAsync(TopicId topicId);
    public Task<Topic> UpdateTopicAsync(Topic topic);
}