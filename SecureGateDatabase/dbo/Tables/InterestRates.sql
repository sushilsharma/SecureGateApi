CREATE TABLE [dbo].[InterestRates] (
    [InterestRateId] BIGINT         NOT NULL,
    [DaysOverdue]    INT            NOT NULL,
    [Rate]           DECIMAL (5, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([InterestRateId] ASC)
);

