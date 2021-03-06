﻿<!doctype html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>Tile</title>
  <link href='http://fonts.googleapis.com/css?family=Roboto+Slab' rel='stylesheet' type='text/css'>
  <link rel="stylesheet" href="style.css">
  <script src="http://code.jquery.com/jquery-1.10.1.min.js"></script>
  <script src="../../external/jquery.animations.min.js"></script>
  <script src="../../src/jquery.animations-tile.js"></script>
  <script src="app.js"></script>
  <script>
      (function (i, s, o, g, r, a, m) {
          i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
              (i[r].q = i[r].q || []).push(arguments)
          }, i[r].l = 1 * new Date(); a = s.createElement(o),
          m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
      })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

      ga('create', 'UA-44208259-3', 'auto');
      ga('send', 'pageview');

  </script>
</head>
<body>
  <div id="header">
    <h1>Tile</h1>
    <a href="https://github.com/emn178/jquery-animations-tile"><img style="position: absolute; top: 0; right: 0; border: 0;" src="https://github-camo.global.ssl.fastly.net/a6677b08c955af8400f44c6298f40e7d19cc5b2d/68747470733a2f2f73332e616d617a6f6e6177732e636f6d2f6769746875622f726962626f6e732f666f726b6d655f72696768745f677261795f3664366436642e706e67" alt="Fork me on GitHub" data-canonical-src="https://s3.amazonaws.com/github/ribbons/forkme_right_gray_6d6d6d.png"></a>
  </div>
  <div id="body">
    <div id="description">
      <p>
        This page shows tile plugin usage. Click button to see the effect and source code.
      </p>
    </div>
    <div id="container">
      <img id="image" src="images/batman.png" />
    </div>
    <div id="settings">
      <button id="submit">Animate</button>
      <div>
        <h3>Pre-defined Combinations</h3>
        <button id="assemble" class="predefined">Assemble</button>
        <button id="blind" class="predefined">Blind</button>
        <button id="wave" class="predefined">Wave</button>
        <button id="flutter" class="predefined">Flutter</button>
        <button id="puzzle" class="predefined">Puzzle</button>
        <button id="flake" class="predefined">Flake</button>
        <button id="blow" class="predefined">Blow</button>
      </div>
      <div id="animations">
        <input id="show-effect" class="toggle-check" type="checkbox"/>
        <div id="effect" class="toggle-wrapper">
          <h3><label class="options-toggle" for="show-effect">Effect</label></h3>
          <div id="effects" class="toggle-container">
            <div>
              <input id="combine" type="checkbox" checked="checked"/>
              <label for="combine">Combine</label>
            </div>
          </div>
        </div>
        <input id="show-alternate" class="toggle-check" type="checkbox"/>
        <div id="alternate" class="toggle-wrapper">
          <h3><label class="options-toggle" for="show-alternate">Alternate</label></h3>
          <div id="alternates" class="toggle-container">
            <div>
              <input id="combine2" type="checkbox" checked="checked"/>
              <label for="combine2">Combine</label>
            </div>
          </div>
        </div>
      </div>
      <input id="show-options" class="toggle-check" type="checkbox"/>
      <div id="options" class="toggle-wrapper">
        <h3><label class="options-toggle" for="show-options">Options</label></h3>
        <div id="options-wrap" class="toggle-container">
          <div class="option-group global">
            <h5>Global</h5>
            <label for="duration">Duration</label>
            <input type="number" id="duration" name="duration" step="100" min="0" />
            <label for="delay">Delay</label>
            <input type="number" id="delay" name="delay" step="100" min="0" />
            <label for="repeat">Repeat</label>
            <input type="number" id="repeat" name="repeat" step="1" min="0" />
            <label for="easing">Easing</label>
            <select id="easing" name="easing">
              <option value=""></option>
              <option value="linear">Linear</option>
              <option value="ease">Ease</option>
              <option value="ease-in">Ease In</option>
              <option value="ease-out">Ease Out</option>
              <option value="ease-in-out">Ease In Out</option>
            </select>
            <label for="direction">Direction</label>
            <select id="direction" name="direction">
              <option value=""></option>
              <option value="normal">Normal</option>
              <option value="alternate">Alternate</option>
              <option value="alternate-reverse">Alternate Reverse</option>
              <option value="reverse">Reverse</option>
            </select>
            <label for="fillMode">Fill Mode</label>
            <select id="fillMode" name="fillMode">
              <option value=""></option>
              <option value="forwards">Forwards</option>
              <option value="backwards">Backwards</option>
              <option value="both">Both</option>
            </select>
            <label for="rows" class="custom">Rows</label>
            <input type="number" id="rows" name="rows" step="1" min="0" />
            <label for="cols" class="custom">Cols</label>
            <input type="number" id="cols" name="cols" step="1" min="0" />
            <label for="groups" class="custom">Groups</label>
            <input type="number" id="groups" name="groups" step="1" min="0" />
            <input id="sequent" name="sequent" type="checkbox" value="true" checked="checked"/>
            <label for="sequent" class="custom">Sequent</label>
            <label for="sequence" class="custom">Sequence</label>
            <select id="sequence" name="sequence">
              <option value=""></option>
              <option value="random">Random</option>
              <option value="randomCols">Random Columns</option>
              <option value="randomRows">Random Rows</option>
              <option value="lr">Left-Right</option>
              <option value="rl">Right-Left</option>
              <option value="tb">Top-Bottom</option>
              <option value="bt">Bottom-Top</option>
              <option value="lrtb">Left-Right Top-Bottom</option>
              <option value="lrbt">Left-Right Bottom-Top</option>
              <option value="rltb">Right-Left Top-Bottom</option>
              <option value="rlbt">Right-Left Bottom-Top</option>
              <option value="tblr">Top-Bottom Left-Right</option>
              <option value="tbrl">Top-Bottom Right-Left</option>
              <option value="btlr">Bottom-Top Left-Right</option>
              <option value="btrl">Bottom-Top Right-Left</option>
            </select>
            <input id="adjustDuration" name="adjustDuration" type="checkbox" value="true" checked="checked"/>
            <label for="adjustDuration" class="custom">Adjust Duration</label>
            <label for="strength" class="custom">*Strength</label>
            <input type="number" id="strength" name="strength" step="1" min="0" />
            <label for="distance" class="custom">*Distance</label>
            <input type="number" id="distance" name="distance" step="1" min="0" />
            <label for="x" class="custom">*X</label>
            <input type="number" id="x" name="x" step="1" />
            <label for="y" class="custom">*Y</label>
            <input type="number" id="y" name="y" step="1" />
            <label for="relative" class="custom">Relative</label>
            <select id="relative" name="relative" data-type="bool">
              <option value=""></option>
              <option value="true">True</option>
              <option value="false">False</option>
            </select>
            <label for="degree" class="custom">*Degree</label>
            <input type="number" id="degree" name="degree" step="1" />
          </div>
        </div>
      </div>
    </div>
    <div id="code-block">
      <h3>Source Code</h3>
      <pre id="code">$('#image').animate('tile');</pre>
    </div>
  </div>
  <div id="footer">
    © 2014 jQuery-animations Demo and image from Wikipedia
  </div>
</body>
</html>
