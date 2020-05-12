# ManiacRhythm alpha v0.1.3 Dev Diary - Vanja Venezia #

## Github Commit Comments ##

Apr 22:

Updated README.md and added blank Unity project. Also added screenshot of the settings from Magic Music Visuals I will be using as a base to make animated backgrounds for songs.

--

Added title / splash screen and updated README.md

--

Added some title splash assets for the first two songs I'd like to implement as well as graphics for note lanes (one player and two player (hopefully!) versions as well as darker versions)

--

Added note graphic and mockup visual for reference

--

Added Canvas with note lane image to project and sized out screen and preview to 1920 x 1080 (my desired resolution)

--

Added fonts and scoring layout graphic. Started rudimentary code for note management. Build some test code to try and spawn notes using my Conductor class and a couple note positions easy to check against the music. The notes spawn at the correct time and within the Canvas element, but not in the correct position. Pretty stumped as to why I can't get them to instantiate above the note lanes like I want, I've tried just about every combination of SetParent, Instantiate, Transform, and Vector3 Position with all of the various Instantiate constructors that I know of to no avail.

--

Started charting one of my songs using a data structure (term used loosely here) I devised to handle note data. I still need to code out a method to read the note chart from a text file but it won't be too difficult. The charting method I've decided on allows me to start charting songs before the program even works properly and know they will function as intended, as the timing system and note reading systems both work. That being said, it took 172 lines of a text file to code out the first 16 bars of a song. It's not too difficult to do but time intensive. I don't have enough time for this project to properly flesh the game out and then build a separate editor program on top, so I've decided on just writing out every note and time code by hand to ensure I have songs charted by the time the engine is working properly. This is to ensure I have enough time to tune the judgement window and timing of the game so it plays well, also if I have extra time I can add more graphical flair, which is fun.

--

Apr 23:

Trying to get notes spawning properly, ended up writing four duplicate scripts for the notes to simplify my problem solving process (ironically). I've had little success. I can get the notes to start moving at the time they're supposed to be at the judgment line rather than 8 beats before it (as I'd currently like), and right now they spawn when theyre supposed to be hit and immediately disappear. Having difficulties with using Lerp, as I have to interpolate the distance to make the game playable (as synced audio and visuals is absolutely key).

--

Fixed an issue in my logic in Note0 only and will use that to test from now on. I will worry about other notes when I can get the first one working. Tried building the project out to see if playing it in the player was the problem but it seems like something is going wrong with my interpolation for the notes as they spawn just above the note lanes and then quickly despawn. When I've gotten them to move normally they fail to despawn so I'm kind of at a loss here.

--

I've got notes working now, at least the first lane of them. Notes now fall according to my float, bool[] system and land at the judgement bar within a few milliseconds of when they should be there (can easily be tuned once everything is implemented and gameplay can be properly tested). Thinking about making them stay spawned a little while longer but turning the object view off so late note presses can still count for something, though every time I've tried to mess with object visibility it's been a bad time so I'm unsure.

It turns out what was wrong with my note spawning was that I was using Vector3s relative to the canvas object that my notes are children of, however the interpolation I'm doing only takes world position into account. Therefore, my notes were spawning where they were supposed to relative to the canvas and then teleporting way out to the left of the screen on the next frame, everything was working correctly though. I've set a rough position for the first note using an empty gameobject as a position reference to find the values I need in the editor.

Next up is fixing my other three note scripts and prefabs and then handling button input and collision checking.

--

Updated readME a bit and tried a new build and run of the game. notes from the build and run: (1) note objects appear and do in fact move down (2) it seems like changing the window size or scaling keeps all UI elements functional and in place but moves the notes and screws up their interpolation.

this means that I need to find some sort of anchor to base my note movement off of potentially, or just figure out what the values need to be for the full resolution build of the game ( / figure out how and why they change).

I'm glad I'm getting through a lot of this troublesome engine construction and tuning very early on in this project. Hoping to have the entire last week before the presentation deadline to polish, meaning basic functionality has to be complete in like one to two weeks.

--

fixed scaling issue by overriding Canvas defaults with Layout Properties. My notes no longer work again, even after reverting them to their original transform values. I think I know how to approach solving this issue now though and it should actually work given that the scaling is fixed.

--

Note0 now works properly and should work whether in the Unity Game tab or built in the Unity Player, I've yet to build a new test version though, however.

Updated readME for more proper attribution to Yu Chao and Graham Tattersall as well as linked a graphical asset I used that requires attribution. Additionally, I've also listed out the two other programs I am using to make assets for this game.

Next order of business is fixing Note1, Note2, and Note3. After that I could either work on hitboxes and judgment or reading charts from text files; I'm currently leaning toward reading charts as it will make testing much easier, considering I have the first 16 bars of the test song charted already.

--

All four note lanes now work, which is great! Bad news is that when I "Build and Run", the notes are still in the wrong place. I've uploaded a gif of this phenomenon ("whyyy.gif")

--

Apr 24:

Had a check in with Adam today and he helped me figure out my issue with difference between transform positions in-editor and post-build. My Game window was set to 16:9 which is the correct aspect ration, but not "Standalone (1920x1080)" which changed the scaling to my export scaling. I took my values and the previous Game window dimensions and calculated what the new values should be ( 1920 * (old_x_pos / old_width) ). Now everything seems to work as it should at this stage. Going to work on loading charts next and then judgment!

Thanks Adam!

--

Currently trying to work out how to read the charts from files. I was originally loading a string from the Unity Editor window that I would then send to a function that would use ReadAllLines() to parse it into an array. I was then trying to (1) cast index 0 as a float and create a new note with it, (2) then read index 1-4 and determine whether or not to set any of the four bools to true. I quickly realized that my format would be perfect for stringstream but more difficult with ReadAllLines. I changed my format to not use any whitespace or newlines and only delimit via commas, this seems like it should work, however I am having trouble casting my strings as floats. I keep getting the error: "FormatException: Input string was not in a correct format." with my cast referenced. I tried float.Parse(temp) with and without the CultureInfo system helpers as well as float.TryParse, which all failed. Not sure how to proceed other than shelving this part for a while and shifting focus to keyboard input and note judgment.

--

Apr 29:

took some time away from this project as I had more pressing due dates and extracurricular circumstances. when I was away though I looked up alternative ways of reading text in C# and found the StreamReader class. upon coming back to the project today, I was able to implement a new chart reading function using the StreamReader class that (1) works perfectly as far as I can tell and (2) allows me to use the original format I designed for charting in a *.txt.

next is player input and note judgment, then scoring, then video background, then menus, then more content. my focus right now is getting the full engine working and having one full song to demonstrate.

--

cleaned the project up before committing last and forgot to change my test chart variation back to the proper naming convention. now it should run properly on test from this commit.

--

Apr 30:

Had some serious github issues due to not knowing there was a max file size for github after trying to push a commit. here are the commit comments that were lost:

    since I already committed the video file, removing it from the project temporarily doesn't work. I added *.mp4 to the gitignore list though so it should actually push to master now I believe.

    -

    Had to remove the video asset from my commit because git keeps failing the upload. Going to try and figure that out but for now I'm just going to keep the video out of my commits, even though it's fully functional.

    -

    Video background is now implemented AND (big one here) time-synced. The documentation about the Video Player component (and especially syncing it with audio separate from the video's audio) is surprisingly hard to come by so I had to experiment with trial and error and figure out how to make it work myself. I would just use the audio from the video itself which is already synced properly, but then I risk the notes de-syncing with the audio which is a much more egregious error, it would defeat the whole purpose of calculating position with dspTime.

    I had planned to work on note judgment and have decided on how I want to implement it (Input.GetKeyDown() and maybe Input.GetKeyUp() if I have time to make hold notes), however, I find that it is best to work on what interests me at the moment as I'll get the work done better and faster; time is of the essence here. Super stoked on getting the video running properly though, I was quite worried about syncing it.

--

added pre-video splash so aesthetic stays consistent

--

Scoring now mostly works, I've implemented a new class and object that handle key input and a series of methods that handle note checking. Combo and Accuracy do not yet function, I'm not exactly sure why.

todo: change +/- 0.1 judgment from hard-coded to SongManager public variable

--

May 1:

I've gotten Combo tracking and accuracy mostly working. They both might be a bit buggy due to how I'm checking, but combo seems like it's working properly from what I can tell and accuracy should be accurate at the end of the song but might drift off slightly mid song due to order of checks (notes can be hit at the same time as they are counted and sometimes this resolves with numNotesHit being greater than NumNotesPassed for a frame. There might some other bugs but it's a difficult thing to pay attention to while testing.

Additionally, I've made a pretty basic little icon for the build, got tired of seeing the default Unity one.

As of right now, the basic core features of the engine are complete.

Now I need to handle ending a song and making a stat splash screen; making more songs and putting them in their own scenes; figuring out how to change scenes from buttons etc; and then making a main menu system with song select and stat tracking.

Gameplay buttons are currently: "d", "f", "j", "k" for the notes, respectively

--

charted another 16 bars of BAD SLIME - THE WORLD BETWEEN; this song is now 1/3 of the way complete.

--

missed a bar before, NOW it's 32 bars charted in total. wanted to get this committed and pushed as its easiest for me to keep track of in musical chunks.

--

I added what I'm calling the "mania multiplier" / "mania system". If the player combo exceeds 100, a x2 multiplier is applied to score until combo is dropped or hits 250. When combo hits 250, the multiplier becomes x4 at that point and will continue until the player misses a note. It also adds a visual multiplier to the Score field in game (ie: "Score: 1750" becomes "Score: 1750 x2", a bit ambiguous but I imagine that most players who have played a rhythm game before will understand that it is applying the multiple to current score additions rather than the total score. I plan to fine tune this once more songs are added and potentially add other possible multipliers.

As of right now I can't quite test this functionality without hard coding a value for combo > 100 or loosening the grading, as the best combo I've achieved is ~78. I am getting better at it though so I imagine that I will be able to get a combo of over 100 by the time the song is finished being charted.

An idea I had that I wanted to note here was a difficulty mode that doesn't change the amount of notes in the chart but instead makes the grading very strict. I plan to call this "Black Label" mode as another stylistic nod to bullet hell games. Black label generally means a stricter more difficult version of a game which (by the nature of the bullet hell genre) usually lends itself to finer, tighter movements in gameplay as the available screen space to dodge in is very low. I thought this was a great translation of this concept. I think some DJ Max games have a stricter judgement option but its a somewhat rare feature so I'm excited to implement it. I think that it wouldn't be entirely unrealistic to attempt to add this mode by the time I have to present this project.

--

twb song chart now half way done, scene renamed "twb"; added a few lines to my checkCombo() method to ensure that even if another check is not incurred beforehand, if combo breaks the multiplier stops.

--

May 2:

I've charted out my song up through the second build up, there seems to be some sort of issue where my very last note pattern for the second buildup spawns much later than it should and im attempting to debug that. I also made some minor changes to the conductor class.

--

tried to fix late note issue by optimizing Conductor a bit and then actually building the game. It did not fix it but I have some ideas for testing to target what's wrong. I think there might be a potential bug with combos sometimes getting dropped out of nowhere but I think it might actually be that I'm missing notes and not noticing at the end of phrases. I really want to implement some sort of 2d particle effect or something of that nature for whenever a note is successfully hit. This would be to make it clear to the player which notes they've hit and which they've missed.

--

May 4:

fixed accuracy calculation, also reorganized conductor slightly with some new variables and streamlining. I fixed the out of bounds error that previously existed, even though it didn't really impact gameplay.

decided to cap demo song at half chart for time, will rechart once game exits alpha v0.1

when song ends, game now transitions into "results" scene which is currently empty but will display max combo, overall accuracy, number of missed notes, and assign a letter grade to the player.

Next order of business is building assets for results screen and getting it functional. After that, title splash screen with an option of "regular demo" or "black label demo" will be constructed.

once all of the above changes are implemented and any bugs are sorted out, game will be officially dubbed alpha v0.1, which will be the build that I present for CS 211.

--

Created a GameObject and script called ScoreSaver to pass data between gameplay scene and results scene.

I've also created a results screen overlay graphic to feed the saved score information into.

Next is creating text objects for the results screen and a script that pull the data from ScoreSaver to fill out the text objects.

--

Results screen is now fully functional and implemented. Next is title screen and black label version of demo scene.

--

I've officially reached alpha v0.1 of ManiacRhythm. I will continue bug testing and optimizing but this is the version I intend to present for CS 211.

Development will continue.

--

May 5:

updated readme with more information and added a link to a video of the current version demo. I also added a screencap of my current highscore on the normal mode of the demo

--

May 6:

Added CS 211 presentation slides

--

Final powerpoint added

--

May 7:

fixed a bug that causes a 100% clear to not register as a Full Combo. Also added a song and chart I've started working on.

--

Charted out "Treasure" more as well as added a graphic and video for it

--

Treasure chart and stage complete. Considering attempting to build a song select screen before presentations tomorrow now that I have more than one complete chart.

--

Added a level select screen for both normal and black label difficulty demos. Also adding some currently unused assets as they may come in handy later.

Version is at alpha v0.1.1 now

--

Added "todo" text file with notes on improvements, optimizations, reorganization, necessary implementations, and future ideas.

--

Wrote a C++ console app that converts a *.csv file to a *.txt file, properly formatted for use with ManiacRhythm.

This was done to make charting slightly easier and it will be easily expandable to accommodate modes with more note types in the future.

--

Updated presentation; modified csvToMrc to hang before exiting; added keycheck to allow looping back to song select from results screen; updated todo.

--

May 8:

Added some screenshots to presentation. Also tried to fix a wrinkle with my csvToMrc app where it fails if there is a newline character at the end of the csv. It is simpler to just manually remove it before running.

--

Fixed bug in Conductor.cs were accuracy, score, and some other internal variables failed to reset after playing a song, causing inaccurate scoring on subsequent songs played.

Also, csvToMrc has reached v0.2. I reorganized the core functionality and added a menu loop that allows the user to convert multiple charts in a row without closing the program or reread and rewrite the last file to the specified location. This second part is especially helpful as it removes the need to restart the program and retype the file paths of the *.csv and *.txt files when working on a chart; i.e. I can set the read file and output destination and then continue to rewrite changes in the *.csv to a *.txt in Assets\Charts as I'm charting.

--

May 9:

Added a readme that outlines gameplay controls and updated todo list

--

May 11:

Finalization of alpha v0.1.3 build. Adjusted judgement value from 0.2 to 0.22, changed formatting of instructions slightly, and updated the changelog to reflect the bug fixes and tweaks since the last version.

--

Added partial dev diary, currently just the majority of Github commit comments. Also updated todo.

Doubled note scroll speed for legibility, this will eventually be adjustable from song select screen (right now default is 2x = 2.0f, previously was 1x = 4.0f)

Added note graphic for "glide" note, this will be a note that does not have to be hit in time, the only requirement is that the correct key is held down when it hits the judgement bar

Created Note prefabs for "glide" versions of normal notes, basically just the same thing with a different icon, as the actual difference will be handled by the judgement check (yet to be implemented)

--

added ModeManager object to title screen to manage mode and song speed globally, this allows me to remove duplicate "_bl" versions of scenes as Conductor now pulls the judgement value directly from the modeManager.

I could probably fold scoreSaver and modeManager into a gameManager class but it doesn't really affect function to keep them separate and it makes more sense to split them to me (as their purposes are quite different, even if they are similar classes)

As of right now, song speed is still not variable, however it shouldn't be difficult to implement once I've decided how to handle it from a UI perspective.

--

Added mouse navigation to song select screen (left and right movement)

## Overall Reflection ##

This project represents a breakthrough moment in my CS education. I am usually an autodidact and sometimes have trouble with the format of school because of it, it is more difficult for me to learn when not primarily self-guided. This project was my chance this semester to push myself and force myself out of my comfort zone. I don't think I would have succeeded in making something with this level of polish and playability if I wasn't super stoked on the idea, so I'm really glad I chose the route I did. I feel like I say this every time, but I've certainly learned more about C# and Unity throughout this process than I have in previous assignments.

The beginning was relatively simple as I had a small amount of starter code to build my ideas and implementation around, even though I began diverging from my original ideas pretty quickly. The most core functionality of the engine, note spawning and checking, was the hardest part (as well as syncing the video backgrouds a bit later on). Getting some input from Adam on the first two check-ins definitely set me up for success, as it allowed me to move psat the two points at which I felt I was beating my head into a wall.

After I came back to the project from nearly a week hiatus midway through, however, it was mostly smooth sailing the rest of the way out. I hit my stride and was balancing my inspiration and drive to work on the project with actually figuring out how to implement things in a reasonable way (and also bug test).

One of my favorite takeaways from this was the StreamReader class in C#, which closely mirrors my favorite tool in C++, the iStringStream and ifStream classes.

The best part of this project for me, however, is now having something to work on over the summer and keep my CS skills sharp. The skills I've learned in Unity will certianly help me with some other projects I have been putting off until I was out of school, which I hope will further my independent CS education, putting me in the best possible position to tackle next semester.

## Media ##

I had originally planned on including a download of a playable build of the game here as well as one without the video to significantly cut down on the file size and also allow it to run better on machines with less power. That is not happening because I didn't get a chance to move my charts into the Resources or StreamingAssets folder and reconfigure my code. If the Unity project is downloaded and the video player is disabled in "twb", "treasure", and "aasb" scenes it should run fine without video.

Here's a video of the gameplay when built into the project folder:

[![](http://img.youtube.com/vi/GTfaaq-FCPU/0.jpg)](http://www.youtube.com/watch?v=GTfaaq-FCPU "ManiacRhythm alpha v0.1.4 [standalone]")

And here's a video of the game running in Unity (apologies for the choppy framerate, recording a game running in Unity is quite resource intensive):

[![](http://img.youtube.com/vi/Jlaw9-Kd0GQ/0.jpg)](http://www.youtube.com/watch?v=Jlaw9-Kd0GQ "ManiacRhythm alpha v0.1.4 [running in Unity]")