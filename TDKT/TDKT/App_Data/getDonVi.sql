USE [TDKT]
GO
/****** Object:  StoredProcedure [dbo].[getDonVi]    Script Date: 7/1/2014 2:22:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[getDonVi]
    @canAudit BIT = NULL
  , @namkt NVARCHAR(5)
AS
    IF @canAudit IS NULL
        BEGIN
            SELECT  ROW_NUMBER() OVER ( ORDER BY MA ) AS 'STT'
                  , MaDonVi = MA
                  , TenDonVi = TEN
                  , CanAudit
                  , BeginDate
                  , EndDate
            FROM    dbo.TD_DVKT
        END
    ELSE
        BEGIN
            SELECT  ROW_NUMBER() OVER ( ORDER BY MA ) AS 'STT'
                  , MaDonVi = MA
                  , TenDonVi = TEN
                  , CanAudit
                  , BeginDate
                  , EndDate
            FROM    dbo.TD_DVKT
            WHERE   ( EndDate IS NOT NULL
                      AND YEAR(EndDate) >= @namkt )
                    OR ( YEAR(BeginDate) <= @namkt )
                    AND CanAudit = 'True'
        END 