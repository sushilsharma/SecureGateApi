
Create FUNCTION [dbo].[GetDateLocal]()
RETURNS DATETIME
AS
BEGIN
    DECLARE @HanoiOffset VARCHAR(6);
    SET @HanoiOffset = '+05:30'; -- Hanoi is UTC+07:00

 

    return CONVERT(DATETIME, SWITCHOFFSET(GETUTCDATE(), @HanoiOffset));
END