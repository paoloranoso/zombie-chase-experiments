zombie-chase-experiments
========================


(project finally migrated from bitbucket to github for posterity/entertainment)


What is this?
-------------
This was a rebuild of our failed game Zombie Chase.  The purpose was to experiment with a few ideas.


Usage
-----
Open the game with [Unity](http://unity3d.com/).  Navigate to the ```Assets/scenes/``` folder and open up ```the main_menu``` scene.
If playing on Desktop, use the WASD keys as your directionals(you can only turn at select areas).  If you were able to build to an iOS device, treat the game like an endless-runner game, swiping to turn or jump.


Experiments
-----------
The experiments we wanted to try in this project were:
* Build out an endless runner version (think Temple Run) and play with that, since our original game consisted of levels.
* Experiment with mixing JS and C# scripts, since the original game was purely C# to play nicely with plugins.  JS was used here to mock up things very quickly(ex: mock up intro scene).
* Experiment with folder and file structure
* Mock up an intro scene for fun

Instead of just creating branches for these experiments, we felt it was better to keep it as an entirely separate project,
in order to keep original repo size low and have this separate repo as our playing ground.  If things went well, this would have become the new main repo, taking only what we needed from the old one.


Discoveries
-----------
* Mixing JS/C# can be done, but it is probably better to keep it pure C#.  JS can be used to mock up quickly, but again, it did not feel that much faster.
* Intro scene was fun!  Should add it to the original repo.
* Endless runner mode success!!!  This will be explained more below.


Endless Runner Mode Explained
-----------------------------
The main idea is to have a bunch of pre-built level pieces.  You want to connect these pieces on the fly at a given time, but not too late so that one can visually notice.  These pieces can then be re-used to give the illusion of an infinite level.
If you are looking at the ```game_scene``` in Unity, you will notice there are areas with certain triggers, where our hero will run through and it will trigger which next level piece to connect to our last piece
Markers are set on the reusable level pieces, so they know how to connect to each other seamlessly.  
Check out ```LevelPieceRecycler.cs``` and ```SpawnLevelPiece.cs``` in ```Assets/scripts/game_scene/``` folder to get a better idea.
