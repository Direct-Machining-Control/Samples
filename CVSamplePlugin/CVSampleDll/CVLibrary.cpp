#include "pch.h"
#include "CVLibrary.h"

int count_pixels(int width, int height, int bytes_per_pixel, char image[])
{
    return width * height;
}

void find_point(int width, int height, int bytes_per_pixel, char image[], int &pointX, int &pointY)
{
    pointX = width / 4;
    pointY = height / 4;
    return;
}
