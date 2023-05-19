#pragma once

#ifdef CVLIBRARY_EXPORTS
#define CVLIBRARY_API __declspec(dllexport)
#else
#define CVLIBRARY_API __declspec(dllimport)
#endif

extern "C" CVLIBRARY_API int count_pixels(int width, int height, int bytes_per_pixel, char image[]);
extern "C" CVLIBRARY_API void find_point(int width, int height, int bytes_per_pixel, char image[], int &pointX, int &pointY);
