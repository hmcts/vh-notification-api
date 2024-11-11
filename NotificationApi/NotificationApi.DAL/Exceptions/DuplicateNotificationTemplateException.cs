using System;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Exceptions;

public class DuplicateNotificationTemplateException(NotificationType notificationType)
    : Exception($"Duplicate entry for notification type {notificationType} found");
