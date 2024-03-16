# Wii U Cafe SDK Deluxe Wiki

Hi ! 
You're on the wiki for Wii U Cafe SDK Deluxe. In the next months, there will be tutorials about using the tool and making game with Wii U Cafe SDK Deluxe and Unity 2017.

## Tutorials
There are no tutorials for now. 
To install Wii U Cafe SDK, Unity and Wii U Dev Kit, please refer to [this tutorial](https://gbatemp.net/threads/make-a-unity-game-for-wii-u.633391/)

## Known issues

Here is a list of known issues in Wii U Cafe SDK Deluxe

###  "This software cannot be used. The Nintendo Network ID used to purchase" when installing or running it on a Wii U

This issue was reported by [Antonios Papadakis (also known as one-and-only)](https://github.com/one-and-only).

This issue is caused by TitleID. But there is a fix :
You need to have the same TitleID in Unity Project Settings, under Player Settings -> Other Settings ->Application section. You have a field named "TitleID (hex)". This needs to match both in Unity and Wii U Cafe SDK Deluxe. You can fill it randomly, but needs to match HEX Format (only 0 -> 9 digits and A -> F letters, and 16 characters long). After making one, check with [Title Database - Wii U Brew](https://wiiubrew.org/wiki/Title_database) if your TitleID does not exist (search it with Control + F on Windows or Command + F on Mac).

**Note** : 
I don't recommend you to use the default one from Unity and Wii U Cafe SDK Deluxe, when you have multiple projects : 
Project A : Already installed on Wii U unit.
Project B : You want to install it to test it
If both have the same TitleID, when installing Project B to the Wii U, files of the Project A will be overwritten to be replaced by the Project B files !

## DUMMY Icon on Wii U Menu 

This issue was reported by [Antonios Papadakis (also known as one-and-only)](https://github.com/one-and-only).

This issue is not currently fixed. I personally don't have it with my example project. I will investigate