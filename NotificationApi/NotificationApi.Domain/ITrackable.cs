using System;
using NotificationApi.Domain.Ddd;

namespace NotificationApi.Domain
{
    public interface ITrackable
    {
        DateTime? CreatedAt { get;  set; }
        DateTime? UpdatedAt { get;  set; }
    }

    public class TrackableEntity<TKey> : Entity<TKey>, ITrackable
    {
        private readonly DateTime _currentUTC = DateTime.UtcNow;
        protected TrackableEntity()
        {
            CreatedAt = _currentUTC;
            UpdatedAt = _currentUTC;
        }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
