CREATE TABLE [dbo].[AdvertisingBanner] (
    [AdvertisingBannerId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [MemberId]            BIGINT         NULL,
    [BannerName]          NVARCHAR (150) NULL,
    [BannerImage]         NVARCHAR (500) NULL,
    [FromDate]            DATETIME       NULL,
    [ToDate]              DATETIME       NULL,
    [Field1]              NVARCHAR (50)  NULL,
    [Field2]              NVARCHAR (50)  NULL,
    [Field3]              NVARCHAR (50)  NULL,
    [Field4]              NVARCHAR (50)  NULL,
    [Field5]              NVARCHAR (50)  NULL,
    [Field6]              NVARCHAR (50)  NULL,
    [Field7]              NVARCHAR (50)  NULL,
    [Field8]              NVARCHAR (50)  NULL,
    [Field9]              NVARCHAR (50)  NULL,
    [Field10]             NVARCHAR (50)  NULL,
    [IsActive]            BIT            NULL,
    [CreatedDate]         DATETIME       CONSTRAINT [DF_AdvertisingBanner_CreatedDate] DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [CreatedBy]           BIGINT         NULL,
    [UpdatedDate]         DATETIME       NULL,
    [UpdatedBy]           BIGINT         NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [Title]               NVARCHAR (500) NULL,
    [Url]                 NVARCHAR (500) NULL,
    [TanentCode]          NVARCHAR (250) NULL,
    CONSTRAINT [PK_AdvertisingBanner] PRIMARY KEY CLUSTERED ([AdvertisingBannerId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique identifier for each advertising banner.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdvertisingBanner', @level2type = N'COLUMN', @level2name = N'AdvertisingBannerId';

