CREATE TABLE [dbo].[user_login_data_external] (
    [UserId]                BIGINT         NOT NULL,
    [ExternalProviderId]    BIGINT         NOT NULL,
    [ExternalProviderToken] NVARCHAR (100) NOT NULL,
    [TanentCode]            NVARCHAR (250) NULL,
    [IsActive]              BIT            NULL,
    [CreatedDate]           DATETIME       NULL,
    [CreatedBy]             BIGINT         NULL,
    [UpdatedDate]           DATETIME       NULL,
    [UpdatedBy]             BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [ExternalProviderId] ASC),
    FOREIGN KEY ([ExternalProviderId]) REFERENCES [dbo].[external_providers] ([ExternalProviderId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[user_account] ([UserId])
);




GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Token provided by the external authentication provider', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data_external', @level2type = N'COLUMN', @level2name = N'ExternalProviderToken';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Identifier of the external provider (Foreign Key)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data_external', @level2type = N'COLUMN', @level2name = N'ExternalProviderId';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Identifier of the user (Foreign Key)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'user_login_data_external', @level2type = N'COLUMN', @level2name = N'UserId';

