CREATE PROCEDURE GenerateMaintenanceBillForAugust
    @FlatId BIGINT
AS
BEGIN
    DECLARE @MemberId BIGINT;
    DECLARE @RoomTypeId BIGINT;
    DECLARE @TotalArea DECIMAL(10, 2);
    DECLARE @FlatNo NVARCHAR(10);
    DECLARE @BillId BIGINT;

    -- Get MemberId, RoomTypeId, FlatNo, and TotalArea from Members table based on FlatId
    SELECT 
        @MemberId = MemberId, 
        @RoomTypeId = RoomTypeId, 
        @TotalArea = TotalArea
    FROM Members
    WHERE FlatId = @FlatId;

    IF @MemberId IS NULL
    BEGIN
        PRINT 'No member found for the given FlatId';
        RETURN;
    END

    -- Insert the Bill
    INSERT INTO Bills (MemberId, UniqueId, FlatId, BillNo, BillDate, PeriodStart, PeriodEnd, DueDate, TotalPayable, PaymentStatus)
    VALUES (
        @MemberId, 
        (SELECT ISNULL(MAX(UniqueId), 1000) + 1 FROM Bills), -- Auto-incrementing UniqueId
        @FlatId, 
        (SELECT ISNULL(MAX(BillNo), 1000) + 1 FROM Bills),   -- Auto-incrementing BillNo
        GETDATE(), 
        '2024-08-01', 
        '2024-08-31', 
        '2024-08-10', 
        0,   -- Initial TotalPayable, will be updated later
        'Pending'
    );

    -- Get the generated BillId
    SET @BillId = SCOPE_IDENTITY();

    -- Calculate Charges based on ChargeTypes and RoomTypeId
    INSERT INTO BillCharges (BillId, ChargeTypeId, Amount)
    SELECT 
        @BillId,
        ChargeTypeId,
        CASE 
            WHEN MethodId = 1 THEN FixedAmount
            WHEN MethodId = 2 THEN RatePerSqFt * @TotalArea
            ELSE 0
        END
    FROM ChargeTypes
    WHERE RoomTypeId IS NULL OR RoomTypeId = @RoomTypeId;

    -- Update the TotalPayable in the Bills table
    UPDATE Bills
    SET TotalPayable = (
        SELECT SUM(Amount) 
        FROM BillCharges 
        WHERE BillId = @BillId
    )
    WHERE BillId = @BillId;

    -- Display the generated bill details
    SELECT 
        b.BillId, b.FlatId, b.BillNo, b.BillDate, b.PeriodStart, b.PeriodEnd, b.DueDate, b.TotalPayable, b.PaymentStatus,
        bc.ChargeTypeId, bc.Amount
    FROM Bills b
    JOIN BillCharges bc ON b.BillId = bc.BillId
    WHERE b.BillId = @BillId;

    PRINT 'Maintenance bill generated successfully for FlatId: ' + CAST(@FlatId AS NVARCHAR);
END;