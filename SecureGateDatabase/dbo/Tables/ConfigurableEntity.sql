CREATE TABLE [dbo].[ConfigurableEntity] (
    [EntityId]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [EntityObjectType] NVARCHAR (250) NOT NULL,
    [EntityObjectId]   BIGINT         NOT NULL,
    [Description]      NVARCHAR (MAX) NULL,
    [State]            BIGINT         NULL,
    [City]             BIGINT         NULL,
    [Pincode]          NVARCHAR (100) NULL,
    [RuleId]           BIGINT         NULL,
    [IsActive]         BIT            NULL,
    [CreatedDate]      DATETIME       DEFAULT ([dbo].[GetDateLocal]()) NULL,
    [CreatedBy]        BIGINT         NULL,
    [UpdatedDate]      DATETIME       NULL,
    [UpdatedBy]        BIGINT         NULL,
    [TanentCode]       NVARCHAR (250) NULL,
    CONSTRAINT [PK_ConfigurableEntity] PRIMARY KEY CLUSTERED ([EntityId] ASC)
);

