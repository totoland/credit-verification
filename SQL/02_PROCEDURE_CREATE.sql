/*** USE [2C2P] ***/
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Toto <Thanapong.N>
-- Create date: 02/12/2018 09:43 PM
-- Description:	Software Architect Technical Assignment <Test>
-- =============================================

ALTER PROCEDURE [dbo].[VerifyCardNumber]
	-- Add the parameters for the stored procedure here
	@CardNo nvarchar(16)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM CREDIT_CARD WHERE CardNumber = @CardNo AND IsActive = 1
END
