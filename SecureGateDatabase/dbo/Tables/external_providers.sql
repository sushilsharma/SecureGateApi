CREATE TABLE [dbo].[external_providers] (
    [ExternalProviderId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProviderName]       NVARCHAR (50)  NOT NULL,
    [WSEndPoint]         NVARCHAR (200) NULL,
    [TanentCode]         NVARCHAR (250) NULL,
    [IsActive]           BIT            NULL,
    [CreatedDate]        DATETIME       NULL,
    [CreatedBy]          BIGINT         NULL,
    [UpdatedDate]        DATETIME       NULL,
    [UpdatedBy]          BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([ExternalProviderId] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Web service endpoint of the external provider', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'external_providers', @level2type = N'COLUMN', @level2name = N'WSEndPoint';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Name of the external provider', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'external_providers', @level2type = N'COLUMN', @level2name = N'ProviderName';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Unique identifier for the external provider', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'external_providers', @level2type = N'COLUMN', @level2name = N'ExternalProviderId';

