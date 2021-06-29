CREATE TABLE [dbo].[TBCOMPROMISSO] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Local]        VARCHAR (100) NULL,
    [Data]         DATETIME      NULL,
    [HoraInicio]   INT           NULL,
    [HoraFim]      INT           NULL,
    [Link]         VARCHAR (100) NULL,
    [MinutoInicio] INT           NULL,
    [MinutoFim]    INT           NULL,
    [Assunto]      VARCHAR (100) NULL,
    [IdContato]    INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

