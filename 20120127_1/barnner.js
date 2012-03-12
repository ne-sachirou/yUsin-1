/**
 * @fileOverview Crain's SpinorNetrork Canvas barnner.
 *   Drawing like Processing.
 * @author ne_Sachirou http://c4se.tk/profile/ne.html
 * @version 2012/0229/
 * @license MIT License
 */

(function () {

/**@constant*/
var LOGOTYPE = new Image(),
    ROOT3 = Math.sqrt(3);
var canvas = document.getElementsByClassName('barnner')[0],
    h1 = document.getElementsByTagName('h1')[0],
    logotypeMode = true,
    context,
    sqrt = Math.sqrt,
    pow = Math.pow,
    ceil = Math.ceil,
    hexagonTiles = [],
    width = 0,
    height = 0,
    fill = null, // {String} fillStyle color
    stroke = null, // {String} strokeStyle color
    alpha = 1, // {Number} globalAlpha
    strokeWidth = 1, // {Number} lineWidth
    strokeCap = 'butt', // {'butt'|'round'|'square'} lineCap
    strokeJoin = 'miter', // {'round'|'bevel'|'miter'} lineJoin
    mouseX = 0,
    mouseY = 0,
    pmouseX = 0, // previous mouseX
    pmouseY = 0; // previous mouseY

if (!canvas) {
  canvas = document.createElement('canvas');
  h1.appendChild(canvas);
  canvas.style.position = 'absolute';
  canvas.style.left = (Number(getComputedStyle(h1).width.slice(0, -2)) - 532) / 2 + 'px';
  logotypeMode = false;
} else {
  LOGOTYPE.src = 'barnner.png';
}
context = canvas.getContext('2d');


/**
 * Set global mouseX, mouseY, pmouseX, pmouseY variable.
 * @param {MouseEvent} evt MouseMoveEvent Object
 */
function getMousePoint (evt) {
  var rect;

  pmouseX = mouseX;
  pmouseY = mouseY;
  if (evt.offsetX) {
    mouseX = evt.offsetX;
    mouseY = evt.offsetY;
  } else if (evt.layerX) {
    mouseX = evt.layerX;
    mouseY = evt.layerY;
  } else {
    rect = evt.target.getBoundingClientRect();
    mouseX = evt.clientX - rect.left;
    mouseY = evt.clientY - rect.top;
  }
}
canvas.addEventListener('mousemove', getMousePoint, false);
canvas.addEventListener('mouseout', function (evt) {
  pmouseX = mouseX;
  pmouseY = mouseY;
}, false);


/**
 * Set Canvas context properties by global Processing like variables.
 */
function adjustEnvironment () {
  var _ctx = context;

  if (fill) {_ctx.fillStyle = fill;}
  if (stroke) {_ctx.strokeStyle = stroke;}
  if (alpha < 0) {alpha = 0;}
  _ctx.globalAlpha = alpha - 0;
  if (strokeWidth < 0) {strokeWidth = 0;}
  _ctx.lineWidth = strokeWidth - 0;
  _ctx.lineCap = strokeCap || 'butt';
  _ctx.lineJoin = strokeJoin || 'miter';
}


/**
 * @param {Function} callbask
 * @return {Function} Stop animation function.
 */
function animate (callback) {
  var timerID,
      stop,
      requestAnimationFrame,
      cancelRequestAnimationFrame;

  function animationFun () {
    adjustEnvironment();
    context.clearRect(0, 0, width, height);
    callback();
  }

  if (window.RequestAnimationFrame) {
    requestAnimationFrame = window.requestAnimationFrame;
    cancelRequestAnimationFrame = window.cancelRequestAnimationFrame;
  } else if (window.mozrequestAnimationFrame) {
    requestAnimationFrame = window.mozRequestAnimationFrame;
    cancelRequestAnimationFrame = window.mozCancelRequestAnimationFrame;
  } else if (window.msRequestAnimationFrame) {
    requestAnimationFrame = window.msRequestAnimationFrame;
    cancelRequestAnimationFrame = window.msCancelRequestAnimationFrame;
  } else if (window.webkitRequestAnimationFrame) {
    requestAnimationFrame = window.webkitRequestAnimationFrame;
    cancelRequestAnimationFrame = window.webkitCancelRequestAnimationFrame;
  } else if (window.oRequestAnimationFrame) {
    requestAnimationFrame = window.oRequestAnimationFrame;
    cancelRequestAnimationFrame = oCancelRequestAnimationFrame;
  }

  if (requestAnimationFrame) {
    timerID = requestAnimationFrame(animationFun);
    stop = function () {
      cancelRequestAnimationFrame(timerID);
    };
  } else {
    timerID = setInterval(animationFun, 16);
    stop = function () {
      clearInterval(timerID);
    };
  }
  return stop;
}


/**
 * @param {Number} w
 * @param {Number} h
 */
function size (w, h) {
  canvas.width = w;
  canvas.height = h;
  width = w;
  height = h;
}


/**
 */
function drawBackground () {
  context.globalAlpha = 0;
  // some will be written in futute
  context.globalAlpha = 1;
}


/**
 * @param {Image} imgObj
 * @param {Number} x top-left
 * @param {Number} y top-left
 * @param {Number} w image width pixel.
 * @param {Number} h image height pixel.
 */
function drawLogotype (imgObj, x, y, w, h) {
  adjustEnvironment();
  context.drawImage(imgObj, x, y, w, h);
}


/**
 * @param {Number} x
 * @param {Number} y
 * @param {number} radius
 */
function drawHexagon (x, y, radius) {
  var _ctx = context;

  _ctx.beginPath();
  _ctx.moveTo(x, y + radius / 2);
  _ctx.lineTo(x, y + radius * 1.5);
  _ctx.lineTo(x + radius * ROOT3 / 2, y + radius * 2);
  _ctx.lineTo(x + radius * ROOT3, y + radius * 1.5);
  _ctx.lineTo(x + radius * ROOT3, y + radius / 2);
  _ctx.lineTo(x + radius * ROOT3 / 2, y);
  _ctx.lineTo(x, y + radius / 2);
  _ctx.closePath();
  adjustEnvironment();
  if (stroke) {
    _ctx.stroke();
  }
  if (fill) {
    _ctx.fill();
  }
}


/**
 * @param {Number} x
 * @param {Number} y
 * @param {Number} radius
 * @return {Boolean}
 */
function isMouseInHexagon (x, y, radius) {
  return sqrt(pow(x + radius * ROOT3 / 2 - mouseX, 2) + pow(y + radius - mouseY, 2)) < radius;
}


/**
 * @param {Number} radius
 * @param {Array} tiles ({String} color, {Number} alpha)[]
 */
function drawHexagonTile (radius, tiles) {
  /**@constant*/
  var GRID_X = radius * ROOT3 / 2,
      GRID_Y = radius * 1.5;
  var tile,
      x = -GRID_X,
      y = -GRID_Y,
      countX = 0,
      countY = 0;

  while (y <= height) {
    while (x <= width) {
      tile = tiles[countX][countY];
      if (tile[1] > 0) {
        tiles[countX][countY][1] = tile[1] - 0.01;
      }
      if (pmouseX !== mouseX && pmouseY !== mouseY &&
          isMouseInHexagon(x, y, radius)) {
        tiles[countX][countY][1] = 1;
      }
      tile = tiles[countX][countY];
      fill = tile[0];
      stroke = tile[0];
      alpha = tile[1];
      drawHexagon(x, y, radius);
      countX += 1;
      x += GRID_X * 2;
    }
    countX = 0;
    countY += 1;
    x = countY % 2 ? 0 : -GRID_X;
    y += GRID_Y;
  }
}


/**
 * Initialize global hexagonTile Array.
 * @param {Number} radius
 */
function initHexagonTiles (radius) {
  var numX = ceil(width / (radius * ROOT3) + 0.5),
      numY = ceil(height / (radius * 1.5)),
      countX = 0,
      countY = 0;

  for (countX = 0; countX <= numX; countX += 1) {
    hexagonTiles[countX] = [];
    for(countY = 0; countY <= numY; countY += 1) {
      hexagonTiles[countX][countY] = ['white', 0];
    }
  }
}


// Main Drawing below.

function setup () {
  size(532, logotypeMode ? 117 : 50);
  initHexagonTiles(16);
}


function draw () {
  drawBackground();
  alpha = 1;
  if (logotypeMode) {
    drawLogotype(LOGOTYPE, 134, 8, width - 268, height - 16);
  }
  drawHexagonTile(16, hexagonTiles);
}


setup();
animate(draw);

}());
