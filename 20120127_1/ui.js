/**
 * @fileOverView UI for yUsin
 * @author ne_Sachirou
 * @version 2012/03/01
 */

(function () {

var buttonMakeNewActivity = document.getElementById('makeNewActivity'),
    menuMakeNewActivity,
    menuMakeNewActivityHeight = 0,
    lockAnimateMenuMakeNewActivity = false,
    timerIdOfShowMenuMakeNewActivity,
    timerIdOfHideMenuMakeNewActivity,
    activityDialog;


/**
 * Convert an Array from Array-like object.
 * @param {Object} obj
 * @return {Array}
 */
function toArray (obj) {
  var i, len,
      result = [];

  for (i = 0, len = obj.length; i < len; i += 1) {
    result.push(obj[i]);
  }
  return result;
}


menuMakeNewActivity = document.createElement('div');
menuMakeNewActivity.setAttribute('id', 'menuMakeNewActivity');
menuMakeNewActivity.innerHTML =
  '<div id="itemMakeNewChar">文字を作る</div>' +
  '<div id="itemMakeNewFont">字体を作る</div>' +
  '<div id="itemMakeNewSentence">例文を作る</div>';
document.body.appendChild(menuMakeNewActivity);


/**
 */
//function ActivityDialog () {
//  if (activityDialog) {
//    return activityDialog;
//  }
//  this.holder = document.getElementById('activityDialogHolder');
//  this.creationArea = '';
//  this.creationAreaChar = document.getElementById('activityCreationAreaChar');
//  this.creationAreaFont = document.getElementById('activityCreationAreaFont');
//  this.creationAreaSentence = document.getElementById('activityCreationAreaSentence');
//}
//
//
//ActivityDialog.prototype = {
//  /**
//   */
//  show: function () {
//    this.holder.style.display = 'block';
//  },
//
//
//  /**
//   */
//  hide: function () {
//    this.holder.style.display = 'none';
//  },
//
//
//  /**
//   */
//  showCharTab: function () {
//    this.creationAreaChar.style.display = 'block';
//    this.creationAreaFont.style.display = 'none';
//    this.creationAreaSentence.style.display = 'none';
//    this.creationArea = 'char';
//  },
//
//
//  /**
//   */
//  showFontTab: function () {
//    this.creationAreaChar.style.display = 'none';
//    this.creationAreaFont.style.display = 'block';
//    this.creationAreaSentence.style.display = 'none';
//    this.creationArea = 'font';
//  },
//
//
//  /**
//   */
//  showSentenceTab: function () {
//    this.creationAreaChar.style.display = 'none';
//    this.creationAreaFont.style.display = 'none';
//    this.creationAreaSentence.style.display = 'block';
//    this.creationArea = 'sentence';
//  },
//
//
//  /**
//   */
//  submit: function () {
//    var createdCharImageFile = document.getElementById('createdCharImageFile');
//
//    switch (this.creationArea) {
//    case 'char':
//      if (createdCharImageFile.value !== '') {
//      } else {
//      }
//      break;
//    case 'font':
//      break;
//    case 'sentence':
//      break;
//    }
//  }
//};
//
//activityDialog = new ActivityDialog();


/**
 * Animation
 */
function showMenuMakeNewActivity () {
  if (menuMakeNewActivityHeight >= 108) {
    clearInterval(timerIdOfShowMenuMakeNewActivity);
    lockAnimateMenuMakeNewActivity = false;
    return;
  }
  lockAnimateMenuMakeNewActivity = true;
  menuMakeNewActivityHeight += 6;
  menuMakeNewActivity.style.height = menuMakeNewActivityHeight + 'px';
}


/**
 * Animation
 */
function hideMenuMakeNewActivity () {
  if (menuMakeNewActivityHeight <= 0) {
    clearInterval(timerIdOfHideMenuMakeNewActivity);
    menuMakeNewActivity.style.display = 'none';
    lockAnimateMenuMakeNewActivity = false;
    return;
  }
  lockAnimateMenuMakeNewActivity = true;
  menuMakeNewActivityHeight -= 6;
  menuMakeNewActivity.style.height = menuMakeNewActivityHeight + 'px';
}


buttonMakeNewActivity.addEventListener('mouseover', function (evt) {
    if (lockAnimateMenuMakeNewActivity) {return;}
    menuMakeNewActivity.style.display = 'block';
    timerIdOfShowMenuMakeNewActivity = setInterval(showMenuMakeNewActivity, 16);
  }, false);

menuMakeNewActivity.addEventListener('mouseout', function (evt) {
    if (lockAnimateMenuMakeNewActivity) {return;}
    timerIdOfHideMenuMakeNewActivity = setInterval(hideMenuMakeNewActivity, 16);
  }, true);

document.getElementById('itemMakeNewChar').
  addEventListener('click', function (evt) {
    //activityDialog.show().showCharTab();
    location.href='char.aspx?new=true';
  }, false);

document.getElementById('itemMakeNewFont').
  addEventListener('click', function (evt) {
    //activityDialog.show().showFontTab();
    location.href = 'font.aspx?new=true';
  }, false);

document.getElementById('itemMakeNewSentence').
  addEventListener('click', function (evt) {
    //activityDialog.show().showSentenceTab();
    location.href = 'sentence.aspx?new=true';
  }, false);

//document.getElementById('buttonCloseActivityDailog').
//  addEventListener('click', function (evt) {
//    activityDialog.hide();
//  }, false);
//
//document.getElementById('buttonSubmitActivity').
//  setAttribute('click', function (evt) {
//    activityDialog.submit();
//  }, false);

}());


/**
 * Convert fonted span-node to fonted images.
 */
(function () {

var fontedNodes,
    fontXmls = {};


/**
 * @param {Element} node
 * @return {XMLDocument}
 */
function getFontXml (node) {
  var xmlDoc,
      fontID = node.getAttribute('data-fontid');

  if (fontID in fontXmls) {
    xmlDoc = fontXmls[fontID];
  }else {
    xmlDoc = document.implementation.createDocument('', '', null);
    xmlDoc.async = false;
    xmlDoc.load('fonts/' + fontID + '.xml');
    fontXmls[fontID] = xmlDoc;
  }
  return xmlDoc;
}


/**
 * @param {Element} node
 */
function fontedOne (node) {
  var i, j, leni, lenj,
      fontXml = getFontXml(node),
      text = node.innerHTML,
      result = '',
      flag = false,
      charactors = fontXml.getElementsByTagName('charactor');

  for (i = 0, leni = text.length; i < leni; i += 1) {
    flag = false;
    for (j = 0, lenj = charactors.length; j < lenj; j += 1) {
      if (charactors[j].getAttribute('unicode') === text[i]) {
        result += '<img src="images/' + charactors[j].getAttribute('imageID') + '.png"  height="64" />';
        flag = true;
        break;
      }
    }
    if (!flag) {
      result += text[i];
    }
  }
  node.innerHTML = result;
  node.style.fontSize = '48px';
  node.style.lineHeight = '48px';
}


/**
 */
function fontedAll () {
  var i, len;

  fontedNodes = document.getElementsByClassName('fonted');
  for (i = 0, len = fontedNodes.length; i < len ; i += 1) {
    fontedOne(fontedNodes[i]);
  }
}


fontedAll();

}());

/**
 */
(function () {

var drawingNode = document.getElementById('drawing'),
    canvas, context;


/**
 */
function drawing () {
  canvas = document.createElement('canvas');
  context = canvas.getContext('2d');
}


if (drawingNode) {
  drawing();
}

}());
