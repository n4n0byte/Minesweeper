CREATE TABLE [dbo].[GameState] (
    [Id]               INT  IDENTITY (1, 1) NOT NULL,
    [player_id]        INT  NOT NULL,
    [game_json]        TEXT NOT NULL,
    [number_of_clicks] INT NOT NULL,
    [seconds_playing] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [fk_user_to_pid] FOREIGN KEY ([player_id]) REFERENCES [Users]([ID])
);