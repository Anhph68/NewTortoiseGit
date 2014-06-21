USE [TDKT]
GO
/****** Object:  StoredProcedure [dbo].[getCuoc]    Script Date: 6/21/2014 5:27:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[getCuoc]
    @namkt VARCHAR(5)
  , @donvi NVARCHAR(30)
AS
    BEGIN
        IF ( @donvi = '' )
            BEGIN
                SELECT  ROW_NUMBER() OVER ( ORDER BY TEN_CUOC ) AS 'STT'
                      , MA_CUOC AS 'MaCuoc'
                      , TEN_CUOC AS 'TenCuoc'
                      , dbo.TD_DVKT.TEN AS N'DonVi'
                      , SO_QD AS 'SoQuyetDinh'
                      , CONVERT(NVARCHAR, NGAY_QD, 103) AS 'NgayKyQD'
                --dbo.TD_LVKT.TEN AS N'linhvuc'
                FROM    dbo.CUOC_KT
                        INNER JOIN dbo.TD_DVKT
                            ON dbo.CUOC_KT.MA_DVKT = dbo.TD_DVKT.MA
                        INNER JOIN dbo.TD_LVKT
                            ON dbo.CUOC_KT.MA_LVKT = dbo.TD_LVKT.MA
                WHERE   NAM_KT = @NamKT
                ORDER BY dbo.TD_DVKT.TEN
                      , TEN_CUOC ASC
            END
        ELSE
            BEGIN
                SELECT  ROW_NUMBER() OVER ( ORDER BY TEN_CUOC ) AS 'STT'
                      , MA_CUOC AS 'MaCuoc'
                      , TEN_CUOC AS 'TenCuoc'
                      , dbo.TD_DVKT.TEN AS N'DonVi'
                      , SO_QD AS 'SoQuyetDinh'
                      , CONVERT(NVARCHAR, NGAY_QD, 103) AS 'NgayKyQD'
                --dbo.TD_LVKT.TEN AS N'linhvuc'
                FROM    dbo.CUOC_KT
                        INNER JOIN dbo.TD_DVKT
                            ON dbo.CUOC_KT.MA_DVKT = dbo.TD_DVKT.MA
                        INNER JOIN dbo.TD_LVKT
                            ON dbo.CUOC_KT.MA_LVKT = dbo.TD_LVKT.MA
                WHERE   NAM_KT = @NamKT
                        AND dbo.TD_DVKT.MA = @donvi
                ORDER BY dbo.TD_DVKT.TEN
                      , TEN_CUOC ASC
            END
    END