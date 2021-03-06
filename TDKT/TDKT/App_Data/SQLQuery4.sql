USE [TDKT]
GO
/****** Object:  StoredProcedure [dbo].[getUsers]    Script Date: 6/20/2014 10:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[getUsers] @donvi NVARCHAR(30)
AS
      BEGIN
            IF (@donvi = '00')
               BEGIN
                     SELECT ROW_NUMBER() OVER (ORDER BY dbo.TD_DVKT.MA, FullName ASC) AS 'STT' ,
                            AspNetUsers.Id AS N'ID' ,
                            FullName AS N'HoTen' ,
                            UserName AS 'TenDangNhap' ,
                            dbo.TD_DVKT.TEN AS N'DonVi'
                     FROM   dbo.AspNetUsers
                            INNER JOIN dbo.TD_DVKT
                                ON TD_DVKT.MA = AspNetUsers.MaDonVi
                     ORDER BY dbo.TD_DVKT.MA ,
                            FullName ASC
               END
            ELSE
               BEGIN
                     SELECT ROW_NUMBER() OVER (ORDER BY dbo.TD_DVKT.MA, FullName ASC) AS 'STT' ,
                            AspNetUsers.Id AS N'ID' ,
                            FullName AS N'HoTen' ,
                            UserName AS 'TenDangNhap' ,
                            dbo.TD_DVKT.TEN AS N'DonVi'
                     FROM   dbo.AspNetUsers
                            INNER JOIN dbo.TD_DVKT
                                ON TD_DVKT.MA = AspNetUsers.MaDonVi
                     WHERE  dbo.TD_DVKT.MA = @donvi
                     ORDER BY dbo.TD_DVKT.MA ,
                            FullName ASC
               END
      END
