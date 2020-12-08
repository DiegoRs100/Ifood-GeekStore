Create Database GeekStore_db

GO

Use GeekStore_db

GO

CREATE TABLE [dbo].[TB_IMAGENS](
	[Id] [uniqueidentifier] NOT NULL,
	[IdUsuarioInclusao] [uniqueidentifier] NOT NULL,
	[DataAlteracao] [datetime2](7) NULL,
	[IdUsuarioAlteracao] [uniqueidentifier] NULL,
	[Ativo] [bit] NOT NULL,
	[Extensao] [varchar](5) NULL,
	[DataInclusao] [datetime2](7) NOT NULL,
	[Nome] [varchar](200) NULL,
 CONSTRAINT [PK_TB_IMAGENS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TB_PRODUTOS](
	[Id] [uniqueidentifier] NOT NULL,
	[IdUsuarioInclusao] [uniqueidentifier] NOT NULL,
	[DataAlteracao] [datetime2](7) NULL,
	[IdUsuarioAlteracao] [uniqueidentifier] NULL,
	[Ativo] [bit] NOT NULL,
	[Nome] [varchar](100) NULL,
	[DataInclusao] [datetime2](7) NOT NULL,
	[IdImagem] [uniqueidentifier] NULL,
	[Preco] [float] NOT NULL,
 CONSTRAINT [PK_TB_PRODUTOS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TB_PRODUTOS]  WITH CHECK ADD CONSTRAINT [FK_TB_PRODUTOS_TB_IMAGENS_IdImagem] FOREIGN KEY([IdImagem])
REFERENCES [dbo].[TB_IMAGENS] ([Id])
GO

ALTER TABLE [dbo].[TB_PRODUTOS] CHECK CONSTRAINT [FK_TB_PRODUTOS_TB_IMAGENS_IdImagem]
GO