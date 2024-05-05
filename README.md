# UWB-CSS-385-Project-5
Project - Hero with Camera

### [Link to WebGL Game](https://wristbandshackles.github.io/UWB-CSS-385-Project-5/Builds/)

# Details
Author: [Christopher Long](https://www.linkedin.com/in/christopher--long/)

Date: 5/3/2024<br>
Professor: Yusuf Pisan<br>
Email: pisan@uw.edu<br>
Due: May 5, 2024<br>
Written in: Unity<br>

Sample project: https://myuwbclasses.github.io/CSS385/MP/CSS385-MP4-Solution.WebGL/Links to an external site.

### Required features:
1. Waypoints upon collision with Eggs, they shake at a frequency of 10 cycles per second, for a varied duration and magnitude
   * First hit shake duration of 1 second, magnitude of 1x1
   * Second hit shake duration of 2 seconds, magnitude of 2x2
   * Third hit shake duration of 3 seconds, magnitude of 3x3
1. Waypoint cam - activated when the waypoint has been shot
   * Waypoint camera has a label indicating its status
1. Enemies when colliding with here will have a counter-clockwise rotation followed by clock-wise rotation followed by going into Chase state. In Chase state the enemy will track the hero as long as the Hero is within 40 units of the enemy.
1. Enemies when colliding with an egg will 1) Go into stunned state and rotate clock-wise until hit again, 2) Upon second collision turn into egg, 3) Upon third collision, respawn
