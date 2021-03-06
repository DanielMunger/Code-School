USE [kickstartdb_test]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[courses_grades_students]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses_grades_students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NULL,
	[grade_id] [int] NULL,
	[student_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[courses_tracks]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses_tracks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NULL,
	[track_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[grades]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[attendance] [varchar](50) NULL,
	[grade] [varchar](25) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[instructors]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[instructors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[instructor_name] [varchar](255) NULL,
	[username] [varchar](255) NULL,
	[password] [varchar](25) NULL,
	[address] [varchar](255) NULL,
	[email] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[instructors_schools]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[instructors_schools](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[instructor_id] [int] NULL,
	[school_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[instructors_tracks]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[instructors_tracks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[instructor_id] [int] NULL,
	[track_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[schools]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[schools](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[school_city] [varchar](255) NULL,
	[school_address] [varchar](233) NULL,
	[phone_number] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[schools_students]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[schools_students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[school_id] [int] NULL,
	[student_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[schools_tracks]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[schools_tracks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[school_id] [int] NULL,
	[track_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](255) NULL,
	[last_name] [varchar](255) NULL,
	[username] [varchar](255) NULL,
	[password] [varchar](25) NULL,
	[address] [varchar](255) NULL,
	[email] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students_tracks]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students_tracks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[student_id] [int] NULL,
	[track_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tracks]    Script Date: 12/19/2016 4:20:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tracks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[track_name] [varchar](255) NULL
) ON [PRIMARY]

GO
