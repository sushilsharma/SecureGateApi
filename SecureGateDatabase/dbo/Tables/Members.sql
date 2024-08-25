CREATE TABLE [dbo].[Members] (
    [MemberId]              BIGINT         NOT NULL,
    [Name]                  NVARCHAR (100) NOT NULL,
    [ContactNumber]         NVARCHAR (100) NOT NULL,
    [Email]                 NVARCHAR (100) NOT NULL,
    [CountryCode]           NVARCHAR (100) NULL,
    [PasswordHash]          NVARCHAR (250) NULL,
    [PasswordSalt]          NVARCHAR (100) NULL,
    [ConfirmationToken]     NVARCHAR (100) NULL,
    [TokenGenerationTime]   DATETIME       CONSTRAINT [DF_Members_TokenGenerationTime] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [PasswordRecoveryToken] NVARCHAR (100) NULL,
    [RecoveryTokenTime]     DATETIME       CONSTRAINT [DF_Members_RecoveryTokenTime] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [FlatId]                BIGINT         NOT NULL,
    [TanentCode]            NVARCHAR (250) NULL,
    [IsActive]              BIT            NULL,
    [CreatedDate]           DATETIME       CONSTRAINT [DF_Members_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [CreatedBy]             BIGINT         NULL,
    [UpdatedDate]           DATETIME       CONSTRAINT [DF_Members_UpdatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [UpdatedBy]             BIGINT         NULL,
    [FailedLoginAttempts]   INT            NULL,
    [LastLoginTime]         DATETIME       NULL,
    [AccountLockTime]       DATETIME       NULL,
    [IsAccountLock]         BIT            NULL,
    CONSTRAINT [PK__Members__0CF04B189C11626E] PRIMARY KEY CLUSTERED ([MemberId] ASC),
    CONSTRAINT [FK__Members__FlatId__76969D2E] FOREIGN KEY ([FlatId]) REFERENCES [dbo].[Flats] ([FlatId])
);






GO
CREATE NONCLUSTERED INDEX [idx_Members_FlatId]
    ON [dbo].[Members]([FlatId] ASC);

