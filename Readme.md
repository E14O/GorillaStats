# Gorilla Server Stats Documentation

## Overview
The "Gorilla Server Stats" mod provides server statistics in the Gorilla Tag game. These stats include details like the lobby code, the number of players, the master client's nickname, the total number of players across all rooms, and the count of tags in the current room.

## Dependencies

- BepInEx

## Installation

To install this mod, place it in the appropriate `BepInEx` plugins folder for Gorilla Tag.

## Features

### 1. Displaying Server Statistics

Once in a server room, a sign in the Forest location will display the following information:
- Lobby Code
- Number of Players in the Room
- Master Client's Nickname
- Total Number of Players across all rooms
- Current server region

## Usage

### Initialization

On game initialization, the mod locates the sign in the Forest location and prepares it for updating the stats. If the sign isn't found, appropriate errors are logged.

### Displaying Stats

The mod constantly updates the sign with the current server statistics. If the player joins a new room or leaves a room, the mod updates the sign accordingly.

## Disclaimer:
<b>This product is not affiliated with Another Axiom Inc. or its videogames Gorilla Tag and Orion Drift and is not endorsed or otherwise sponsored by Another Axiom. Portions of the materials contained herein are property of Another Axiom. ©2021 Another Axiom Inc.</b>
