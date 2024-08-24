CREATE TABLE [dbo].[hashing_algorithms] (
    [HashAlgorithmId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [AlgorithmName]   NVARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([HashAlgorithmId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Name of the hashing algorithm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'hashing_algorithms', @level2type = N'COLUMN', @level2name = N'AlgorithmName';


GO
EXECUTE sp_addextendedproperty @name = N'Comment', @value = N'Unique identifier for the hashing algorithm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'hashing_algorithms', @level2type = N'COLUMN', @level2name = N'HashAlgorithmId';

