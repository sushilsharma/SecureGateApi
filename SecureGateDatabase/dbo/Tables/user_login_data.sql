CREATE TABLE [dbo].[user_login_data] (
    [UserId]                  BIGINT         NOT NULL,
    [LoginName]               NVARCHAR (20)  NOT NULL,
    [PasswordHash]            NVARCHAR (250) NOT NULL,
    [PasswordSalt]            NVARCHAR (100) NOT NULL,
    [HashAlgorithmId]         BIGINT         NOT NULL,
    [EmailAddress]            NVARCHAR (100) NOT NULL,
    [ConfirmationToken]       NVARCHAR (100) NULL,
    [TokenGenerationTime]     DATETIME       NULL,
    [EmailValidationStatusId] BIGINT         NULL,
    [PasswordRecoveryToken]   NVARCHAR (100) NULL,
    [RecoveryTokenTime]       DATETIME       NULL,
    [TanentCode]              NVARCHAR (250) NULL,
    [IsActive]                BIT            NULL,
    [CreatedDate]             DATETIME       NULL,
    [CreatedBy]               BIGINT         NULL,
    [UpdatedDate]             DATETIME       NULL,
    [UpdatedBy]               BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    FOREIGN KEY ([EmailValidationStatusId]) REFERENCES [dbo].[email_validation_status] ([EmailValidationStatusId]),
    FOREIGN KEY ([HashAlgorithmId]) REFERENCES [dbo].[hashing_algorithms] ([HashAlgorithmId])
);




GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Time when the password recovery token was generated', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'RecoveryTokenTime';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Token used for password recovery', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'PasswordRecoveryToken';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Status identifier indicating if the users email is validated', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'EmailValidationStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Time when the confirmation token was generated', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'TokenGenerationTime';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Token used for confirming the users email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'ConfirmationToken';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Email address of the user', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'EmailAddress';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Identifier of the hashing algorithm used', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'HashAlgorithmId';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Salt used for hashing the password', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'PasswordSalt';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Hashed password', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'PasswordHash';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Username used for login', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'LoginName';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Unique identifier for each users login data (matches user_account.UserId)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data', @level2type = N'COLUMN', @level2name = N'UserId';

