using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

Raylib.InitWindow(800,600, "ObstacleGame");
Raylib.SetTargetFPS(60);

float speed = 3.5f;

Texture2D playerImage = Raylib.LoadTexture("pirate.png");
Rectangle playerRect = new Rectangle(10, 20, playerImage.width, playerImage.height);

Texture2D shooterImage = Raylib.LoadTexture("cannon.png");
Rectangle shooterRect = new Rectangle(200, 10, shooterImage.width, shooterImage.height);

Texture2D obstacleImage = Raylib.LoadTexture("cannonball.png");
Rectangle obstacleRect = new Rectangle(200, 50, obstacleImage.width, obstacleImage.height);


List<Rectangle> snowflakes = new List<Rectangle>();
List<float> snowSpeed = new List<float>();
Random generator = new Random();

bool undoX = false;
bool undoY = false;

Vector2 movement = new Vector2();

string level = "stage1";


for (int i = 0; i < 2500; i++)
{
    int x = generator.Next (Raylib.GetScreenWidth());
    int y = generator.Next (Raylib.GetScreenHeight());
    int size = generator.Next(2, 5);
    snowflakes.Add(new Rectangle (x, y, size, size));

    float Speed = (float) (generator.NextDouble() + 0.5);
    snowSpeed.Add(speed);
}

while (!Raylib.WindowShouldClose())
{
    undoX = false;
    undoY = false;

    if (level == "stage1" || level == "stage2")
{
        movement = ReadMovement(speed);
        playerRect.x += movement.X;
        playerRect.y += movement.Y;

    if (playerRect.x < 0 || playerRect.x + playerRect.width > Raylib.GetScreenWidth())
    {
        undoX = true;
    }
    if (playerRect.y < 0 || playerRect.y + playerRect.height > Raylib.GetScreenHeight())
    {
        undoY = true;
    }
}

if (level == "stage1")
{
    if (Raylib.CheckCollisionRecs(playerRect, obstacleRect))
    {
        level = "end";
    }
    if (playerRect.x > 800)
    {
        level = "stage2";
        playerRect.x = 0;
    }
}
else if (level == "stage2")
{
    if (playerRect.x < 0)
    {
        level = "stage1";
        playerRect.x = 800 - playerRect.width;
    }
}

if (undoX == true) playerRect.x -= movement.X;
if (undoY == true) playerRect.y -= movement.Y;

Raylib.BeginDrawing();

if (level == "stage1" || level == "stage2")
{
    for (int i = 0; i < snowflakes.Count; i++)
    {
    Rectangle flake = snowflakes[i];
    flake.y += snowSpeed[i];

    if (flake.y > Raylib.GetScreenHeight())
    {
        flake.y = -10;
    }
    snowflakes[i] = flake;

    Raylib.DrawRectangleRec(snowflakes[i], Color.GRAY);

    Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
    Raylib.DrawTexture(shooterImage, 187, -1, Color.WHITE);
    Raylib.DrawTexture(obstacleImage, 600, 50, Color.WHITE);
    Raylib.DrawTexture(obstacleImage, 400, 50, Color.WHITE);
    Raylib.DrawTexture(obstacleImage, 200, 50, Color.WHITE);
    Raylib.ClearBackground(Color.BLACK);

    }

}
else if (level == "end")
{
    Raylib.ClearBackground(Color.RED);
    Raylib.DrawText("YOU*RE GARBAGE", 20, 300, 20, Color.BLACK);
}

Raylib.EndDrawing();

}

static Vector2 ReadMovement(float speed)
{
    Vector2 movement = new Vector2();
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) movement.Y = -speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) movement.Y = speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) movement.X = -speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) movement.X = speed;     

    return movement;
}


