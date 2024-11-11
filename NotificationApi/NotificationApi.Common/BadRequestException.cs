using System;

namespace NotificationApi.Common;

/// <summary>
/// Exception to throw when input data passed downstream from the api input is in an invalid format
/// </summary>
public class BadRequestException(string message) : Exception(message);
