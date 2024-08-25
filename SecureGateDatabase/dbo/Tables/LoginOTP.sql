CREATE TABLE [dbo].[LoginOTP] (
    [LoginOTPId]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]                  BIGINT         NOT NULL,
    [RoleMasterId]            INT            NOT NULL,
    [OTPGenerated]            NVARCHAR (50)  NOT NULL,
    [OTPCreatedTime]          DATETIME       CONSTRAINT [DF_LoginOTP_OTPCreatedTime] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [OTPValidTill]            DATETIME       CONSTRAINT [DF_LoginOTP_OTPValidTill] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [TanentCode]              NVARCHAR (250) NOT NULL,
    [IsActive]                BIT            NOT NULL,
    [CreatedBy]               BIGINT         NOT NULL,
    [CreatedDate]             DATETIME       CONSTRAINT [DF_LoginOTP_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [ModifiedBy]              BIGINT         NULL,
    [ModifiedDate]            DATETIME       CONSTRAINT [DF_LoginOTP_ModifiedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [IsOTPUsed]               BIT            CONSTRAINT [DF_LoginOTP_IsOTPUsed] DEFAULT ((0)) NOT NULL,
    [IsCurrentOTPRegenerated] BIT            CONSTRAINT [DF_LoginOTP_IsCurrentOTPRegenerated] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LoginOTP] PRIMARY KEY CLUSTERED ([LoginOTPId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the current OTP is regenerated (bit).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'IsCurrentOTPRegenerated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the OTP is used (bit).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'IsOTPUsed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the OTP was last modified (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who last modified the OTP (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'ModifiedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the OTP was created (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'CreatedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user who created the OTP (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'CreatedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicates whether the OTP is active (bit).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'IsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time until which the OTP is valid (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'OTPValidTill';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time when the OTP was generated (datetime).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'OTPCreatedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Generated OTP (nvarchar(50)).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'OTPGenerated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the role master (int).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'RoleMasterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifier of the user associated with the OTP (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for each login OTP (bigint).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LoginOTP', @level2type = N'COLUMN', @level2name = N'LoginOTPId';

