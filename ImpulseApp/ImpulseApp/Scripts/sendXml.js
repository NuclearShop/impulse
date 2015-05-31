function flatten(obj) {
    var result = Object.create(obj);
    for (var key in result) {
        result[key] = result[key];
    }
    return result;
}

//Нужно использовать именно эти прототипы с такими названиями классов. Так как по названию идет связывание с моделью на сервере
var SimpleAdModelDTO = function () { }
SimpleAdModelDTO.prototype = {
    Name: 'SAM',
    HtmlStartSource: '',
    HtmlEndSource: '',
    AdStates: new Array(),
    StateGraph: new Array(),
    Id: 0
};

var AdStateDTO = function () { }
AdStateDTO.prototype = {
    EndTime: -1,
    ChainedHtml: '',
    IsFullPlay: true,
    IsStart: false,
    VideoUnitId: 0,
    IsEnd: false,
    Name: '',
    UserElements: new Array()
}

var UserElementDTO = function () { }
UserElementDTO.prototype = {
    HtmlId: '',
    HtmlClass: '',
    UseDefaultStyle: true,
    HtmlStyle: '',
    Text: '',
    X: 0,
    Y: 0,
    Width: 0,
    Height: 0,
    Action: 0,
    TimeAppear: 0,
    TimeDisappear: 0,
    CurrentId: 0,
    NextId: 0,
    NextTime: 0,
    FormName: '',
    HtmlType: '',
    HtmlTags: new Array()
}

var HtmlTagDTO = function () { };
HtmlTagDTO.prototype = {
    key: '',
    value: ''
};

var NodeLink = function () { };
NodeLink.prototype = {
    V1: 0,
    V2: 0,
    T: 0
};

var SenderStub = function () { };
SenderStub.send =  function() {
    var video1 = new AdStateDTO();
    video1.VideoUnitId = 68;
    video1.IsStart = true;
    video1.IsFullPlay = false;
    video1.EndTime = 10;
    video1.DefaultNext = 66;

    var element1 = new UserElementDTO();
    element1 = flatten(element1);
    element1.HtmlClass = 'mpls-action-button';
    element1.Action = 'next-slide';
    element1.CurrentId = 68;
    element1.NextId = 66;
    element1.NextTime = 0;
    element1.TimeAppear = 3;
    element1.TimeDisappear = 8;
    element1.Text = 'Перейти к видео 3';
    element1.Width = 68;
    element1.Height = 10;
    element1.X = 50;
    element1.Y = 50;
    element1.HtmlType = 'div';
    

    var element2 = new UserElementDTO();
    element2 = flatten(element2);
    element2.HtmlClass = 'mpls-action-button';
    element2.Action = 'next-slide';
    element2.CurrentId = 68;
    element2.NextId = 66;
    element2.NextTime = 3;
    element2.TimeAppear = 3;
    element2.TimeDisappear = 8;
    element2.Text = 'Перейти к видео 3 на позицию 3c';
    element2.Width = 10;
    element2.Height = 5;
    element2.X = 10;
    element2.Y = 5;
    element2.HtmlType = 'div';

    video1.UserElements = [element1, element2];
    /*video1.ChainedHtml =
        "<button class='mpls-action-button' data-action='next-slide' data-current-id='68' data-next-id='66' data-next-time='0'>Перейти к видео 3</button>" +
        "<button class='mpls-action-button' data-action='next-slide' data-current-id='68' data-next-id='66' data-next-time='3'>Перейти к видео 3 на позицию 3c</button>";*/
    video1.Name = 'StartState';


    var video2 = new AdStateDTO();
    video2.VideoUnitId = 67;
    video2.IsFullPlay = false;
    video2.EndTime = 10;
    var element3 = new UserElementDTO();
    element3 = flatten(element3);
    element3.HtmlClass = 'mpls-decorate';
    element3.TimeAppear = -1;
    element3.TimeDisappear = -1;
    element3.Text = 'Конец';
    element3.HtmlType = 'span';
    element3.X = 68;
    element3.Y = 68;

    var element4 = new UserElementDTO();
    element4 = flatten(element3);
    element4.HtmlClass = 'mpls-input';
    element4.TimeAppear = -1;
    element4.TimeDisappear = -1;
    element4.HtmlType = 'input';
    element4.X = 50;
    element4.Y = 50;

    var elementfourTags1 = new HtmlTagDTO();
    elementfourTags1.key = 'type';
    elementfourTags1.value = 'text';

    var elementfourTags2 = new HtmlTagDTO();
    elementfourTags2.key = 'placeholder';
    elementfourTags2.value = 'Надо что-то ввести';

    element4.HtmlTags = [elementfourTags1, elementfourTags2];

    video2.UserElements = [element3, element4];
    /*video2.ChainedHtml =
        "<span>Конец</span>"
    "<input type='text' placeholder='Надо что-то ввести'/>";*/
    video2.IsFullPlay = true;
    video2.IsEnd = true;
    video2.Name = 'EndState';


    var video3 = new AdStateDTO();
    video3.VideoUnitId = 66;
    video3.IsFullPlay = false;
    video3.EndTime = 10;
    video3.DefaultNext = 67;

    var element5 = new UserElementDTO();
    element5 = flatten(element5);
    element5.HtmlClass = 'mpls-action-button';
    element5.Action = 'next-slide';
    element5.CurrentId = 66;
    element5.NextId = 67;
    element5.NextTime = 0;
    element5.TimeAppear = 2;
    element5.TimeDisappear = 8;
    element5.Text = 'Перейти к видео 2';
    element5.Width = 68;
    element5.Height = 10;
    element5.X = 50;
    element5.Y = 50;
    element5.HtmlType = 'div';

    var element6 = new UserElementDTO();
    element6 = flatten(element6);
    element6.HtmlClass = 'mpls-action-button';
    element6.Action = 'next-slide';
    element6.CurrentId = 66;
    element6.NextId = 68;
    element6.NextTime = 12;
    element6.TimeAppear = 2;
    element6.TimeDisappear = 8;
    element6.Text = 'Перейти к видео 1 на позицию 12c';
    element6.Width = 68;
    element6.Height = 10;
    element6.X = 10;
    element6.Y = 50;
    element6.HtmlType = 'div';

    video3.UserElements = [element5, element6];

    /*video3.ChainedHtml =
        "<button class='mpls-action-button' data-action='next-slide' data-current-id='66' data-next-id='67' data-next-time='0'>Перейти к видео 2</button>"
    "<button class='mpls-action-button' data-action='next-slide' data-current-id='66' data-next-id='68' data-next-time='12'>Перейти к видео 1 на позицию 12c</button>";*/
    video3.Name = 'MiddleState';


    var ad = new SimpleAdModelDTO();
    ad.Name = "Nop";
    ad.AdStates = new Array(video1, video2, video3);
    ad.HtmlStartSource = "<button data-action='start' class='mpls-action-button mpls-action-button-start'>Начать воспроизведение</button>";
    var link1 = new NodeLink();
    var link2 = new NodeLink();
    var link3 = new NodeLink();
    var link4 = new NodeLink();

    link1.V1 = 68;
    link1.V2 = 66;
    link1.T = 0;

    link2.V1 = 68;
    link2.V2 = 66;
    link2.T = 3;

    link3.V1 = 66;
    link3.V2 = 67;
    link3.T = 0;

    link4.V1 = 66;
    link4.V2 = 68;
    link4.T = 12;

    ad.StateGraph = [link1, link2, link3, link4];
    //ad.ShortUrlKey = '12088';
    //ad.Id = 13;


    var modelDTO = JSON.stringify(ad);
    return ad;
    
}