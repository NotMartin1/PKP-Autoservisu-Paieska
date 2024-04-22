﻿namespace Model.Entities.Authorization
{
    public enum RegistrationResultCode
    {
        DuplicateUsername,
        ValidationFailed,
        InvalidPhoneNumber,
        InvalidEmail,
        Success,
    }
}
