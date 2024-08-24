CREATE TABLE [dbo].[InterestRates] (
    [InterestRateId] BIGINT         NOT NULL,
    [DaysOverdue]    INT            NOT NULL,
    [Rate]           DECIMAL (5, 2) NOT NULL,
    [TanentCode]     NVARCHAR (250) NULL,
    [IsActive]       BIT            NULL,
    [CreatedDate]    DATETIME       NULL,
    [CreatedBy]      BIGINT         NULL,
    [UpdatedDate]    DATETIME       NULL,
    [UpdatedBy]      BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([InterestRateId] ASC)
);



