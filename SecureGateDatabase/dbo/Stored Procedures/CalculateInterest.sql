CREATE PROCEDURE CalculateInterest
AS
BEGIN
    UPDATE Bills
    SET InterestAmount = CASE
        WHEN DATEDIFF(DAY, DueDate, GETDATE()) > 0 THEN 
            (TotalPayable * (SELECT TOP 1 Rate 
                             FROM InterestRates 
                             WHERE DaysOverdue <= DATEDIFF(DAY, DueDate, GETDATE())
                             ORDER BY DaysOverdue DESC) / 100)
        ELSE 0
        END,
        TotalWithInterest = TotalPayable + ISNULL(InterestAmount, 0)
    WHERE PaymentStatus = 'Pending' AND DATEDIFF(DAY, DueDate, GETDATE()) > 0;
END;