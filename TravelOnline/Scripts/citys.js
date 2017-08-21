var CityArr = [
    ["CategoryName", "ParentId", "Id"],
    ["华北地区", "0", "1"],
    ["北京", "1", "109"],
    ["北京", "109", "101010100", "B"],
    ["天津", "1", "110"],
    ["天津", "110", "101030100", "T"],
    ["河北", "1", "111"],
    ["石家庄", "111", "101090101", "S"],
    ["保定", "111", "101090201", "B"],
    ["承德市", "111", "101090402", "C"],
    ["沧州", "111", "101090701", "C"],
    ["衡水", "111", "101090801", "H"],
    ["邯郸", "111", "101091001", "H"],
    ["廊坊", "111", "101090601", "L"],
    ["秦皇岛", "111", "101091101", "Q"],
    ["唐山", "111", "101090501", "T"],
    ["邢台", "111", "101090901", "X"],
    ["张家口", "111", "101090301", "Z"],
    ["山西", "1", "112"],
    ["太原", "112", "101100101", "T"],
    ["长治", "112", "101100501", "C"],
    ["大同", "112", "101100201", "D"],
    ["晋中", "112", "101100401", "J"],
    ["晋城", "112", "101100601", "J"],
    ["临汾", "112", "101100701", "L"],
    ["吕梁", "112", "101101100", "L"],
    ["忻州", "112", "101101001", "X"],
    ["阳泉", "112", "101100301", "Y"],
    ["运城", "112", "101100801", "Y"],
    ["朔州", "112", "101100901", "Y"],
    ["内蒙古", "1", "113"],
    ["呼和浩特", "113", "101080101", "H"],
    ["阿拉善左旗", "113", "101081201", "A"],
    ["包头", "113", "101080201", "B"],
    ["赤峰", "113", "101080601", "C"],
    ["鄂尔多斯", "113", "101080701", "E"],
    ["呼伦贝尔", "113", "101081000", "H"],
    ["集宁", "113", "101080401", "J"],
    ["临河", "113", "101080801", "L"],
    ["通辽", "113", "101080501", "T"],
    ["乌海", "113", "101080301", "W"],
    ["乌兰浩特", "113", "101081101", "W"],
    ["锡林浩特", "113", "101080901", "X"],
    ["东北地区", "0", "2"],
    ["辽宁", "2", "114"],
    ["沈阳", "114", "101070101", "S"],
    ["鞍山", "114", "101070301", "A"],
    ["本溪", "114", "101070501", "B"],
    ["朝阳", "114", "101071201", "C"],
    ["大连", "114", "101070201", "D"],
    ["丹东", "114", "101070601", "D"],
    ["抚顺", "114", "101070401", "F"],
    ["阜新", "114", "101070901", "F"],
    ["葫芦岛", "114", "101071401", "H"],
    ["锦州", "114", "101070701", "J"],
    ["辽阳", "114", "101071001", "L"],
    ["盘锦", "114", "101071301", "P"],
    ["铁岭", "114", "101071101", "T"],
    ["营口", "114", "101070801", "Y"],
    ["吉林", "2", "115"],
    ["长春", "115", "101060101", "C"],
    ["白城", "115", "101060601", "B"],
    ["白山", "115", "101060901", "B"],
    ["吉林", "115", "101060201", "J"],
    ["辽源", "115", "101060701", "L"],
    ["四平", "115", "101060401", "S"],
    ["松原", "115", "101060801", "S"],
    ["通化", "115", "101060501", "T"],
    ["延吉", "115", "101060301", "Y"],
    ["黑龙江", "2", "116"],
    ["哈尔滨", "116", "101050101", "H"],
    ["大兴安岭", "116", "101050701", "D"],
    ["大庆", "116", "101050901", "D"],
    ["黑河", "116", "101050601", "H"],
    ["鹤岗", "116", "101051201", "H"],
    ["佳木斯", "116", "101050401", "J"],
    ["鸡西", "116", "101051101", "J"],
    ["牡丹江", "116", "101050301", "M"],
    ["齐齐哈尔", "116", "101050201", "Q"],
    ["七台河", "116", "101051002", "Q"],
    ["绥化", "116", "101050501", "S"],
    ["伊春", "116", "101050801", "Y"],
    ["双鸭山", "116", "101051301", "S"],
    ["华东地区", "0", "3"],
    ["上海", "3", "117"],
    ["上海", "117", "101020100", "S"],
    ["山东", "3", "118"],
    ["济南", "118", "101120101", "J"],
    ["滨州", "118", "101121101", "B"],
    ["德州", "118", "101120401", "D"],
    ["东营", "118", "101121201", "D"],
    ["菏泽", "118", "101121001", "H"],
    ["济宁", "118", "101120701", "J"],
    ["临沂", "118", "101120901", "L"],
    ["莱芜", "118", "101121601", "L"],
    ["聊城", "118", "101121701", "L"],
    ["青岛", "118", "101120201", "Q"],
    ["潍坊", "118", "101120601", "W"],
    ["威海", "118", "101121301", "W"],
    ["烟台", "118", "101120501", "Y"],
    ["日照", "118", "101121501", "R"],
    ["泰安", "118", "101120801", "T"],
    ["淄博", "118", "101120301", "Z"],
    ["枣庄", "118", "101121401", "Z"],
    ["安徽", "3", "119"],
    ["合肥", "119", "101220101", "H"],
    ["安庆", "119", "101220601", "A"],
    ["蚌埠", "119", "101220201", "B"],
    ["亳州", "119", "101220901", "B"],
    ["滁州", "119", "101221101", "C"],
    ["巢湖", "119", "101221601", "C"],
    ["池州", "119", "101221701", "C"],
    ["阜阳", "119", "101220801", "F"],
    ["淮南", "119", "101220401", "H"],
    ["黄山", "119", "101221001", "H"],
    ["淮北", "119", "101221201", "H"],
    ["六安", "119", "101221501", "L"],
    ["马鞍山", "119", "101220501", "M"],
    ["宿州", "119", "101220701", "S"],
    ["铜陵", "119", "101221301", "T"],
    ["芜湖", "119", "101220301", "W"],
    ["宣城", "119", "101221401", "X"],
    ["江苏", "3", "120"],
    ["南京", "120", "101190101", "N"],
    ["常州", "120", "101191101", "C"],
    ["南通", "120", "101190501", "N"],
    ["淮安", "120", "101190901", "H"],
    ["连云港", "120", "101191001", "L"],
    ["苏州", "120", "101190401", "S"],
    ["宿迁", "120", "101191301", "S"],
    ["泰州", "120", "101191201", "T"],
    ["无锡", "120", "101190201", "W"],
    ["徐州", "120", "101190801", "X"],
    ["扬州", "120", "101190601", "Y"],
    ["盐城", "120", "101190701", "Y"],
    ["镇江", "120", "101190301", "Z"],
    ["浙江", "3", "121"],
    ["杭州", "121", "101210101", "H"],
    ["湖州", "121", "101210201", "H"],
    ["嘉兴", "121", "101210301", "J"],
    ["金华", "121", "101210901", "J"],
    ["丽水", "121", "101210801", "L"],
    ["宁波", "121", "101210401", "N"],
    ["衢州", "121", "101211001", "Q"],
    ["绍兴", "121", "101210501", "S"],
    ["台州", "121", "101210601", "T"],
    ["温州", "121", "101210701", "W"],
    ["舟山", "121", "101211101", "Z"],
    ["江西", "3", "122"],
    ["南昌", "122", "101240101", "N"],
    ["抚州", "122", "101240401", "F"],
    ["赣州", "122", "101240701", "G"],
    ["九江", "122", "101240201", "J"],
    ["吉安", "122", "101240601", "J"],
    ["景德镇", "122", "101240801", "J"],
    ["萍乡", "122", "101240901", "P"],
    ["上饶", "122", "101240301", "S"],
    ["新余", "122", "101241001", "X"],
    ["宜春", "122", "101240501", "Y"],
    ["鹰潭", "122", "101241101", "Y"],
    ["福建", "3", "123"],
    ["福州", "123", "101230101", "F"],
    ["龙岩", "123", "101230701", "L"],
    ["宁德", "123", "101230301", "N"],
    ["南平", "123", "101230901", "N"],
    ["莆田", "123", "101230401", "P"],
    ["泉州", "123", "101230501", "Q"],
    ["三明", "123", "101230801", "S"],
    ["厦门", "123", "101230201", "X"],
    ["漳州", "123", "101230601", "Z"],
    ["中南地区", "0", "4"],
    ["河南", "4", "124"],
    ["郑州", "124", "101180101", "Z"],
    ["安阳", "124", "101180201", "A"],
    ["鹤壁", "124", "101181201", "H"],
    ["焦作", "124", "101181101", "J"],
    ["济源", "124", "101181801", "J"],
    ["开封", "124", "101180801", "K"],
    ["洛阳", "124", "101180901", "L"],
    ["漯河", "124", "101181501", "L"],
    ["南阳", "124", "101180701", "N"],
    ["平顶山", "124", "101180501", "P"],
    ["濮阳", "124", "101181301", "P"],
    ["商丘", "124", "101181001", "S"],
    ["三门峡", "124", "101181701", "S"],
    ["信阳", "124", "101180601", "X"],
    ["新乡", "124", "101180301", "X"],
    ["许昌", "124", "101180401", "X"],
    ["周口", "124", "101181401", "Z"],
    ["驻马店", "124", "101181601", "Z"],
    ["湖北", "4", "125"],
    ["武汉", "125", "101200101", "W"],
    ["鄂州", "125", "101200301", "E"],
    ["恩施", "125", "101201001", "E"],
    ["黄冈", "125", "101200501", "H"],
    ["黄石", "125", "101200601", "H"],
    ["荆州", "125", "101200801", "J"],
    ["荆门", "125", "101201401", "J"],
    ["潜江", "125", "101201701", "Q"],
    ["十堰", "125", "101201101", "S"],
    ["神农架", "125", "101201201", "S"],
    ["随州", "125", "101201301", "S"],
    ["天门", "125", "101201501", "T"],
    ["襄樊", "125", "101200201", "X"],
    ["孝感", "125", "101200401", "X"],
    ["咸宁", "125", "101200701", "X"],
    ["仙桃", "125", "101201601", "X"],
    ["宜昌", "125", "101200901", "Y"],
    ["湖南", "4", "126"],
    ["长沙", "126", "101250101", "C"],
    ["郴州", "126", "101250501", "C"],
    ["常德", "126", "101250601", "C"],
    ["衡阳", "126", "101250401", "H"],
    ["怀化", "126", "101251201", "H"],
    ["吉首", "126", "101251501", "J"],
    ["娄底", "126", "101250801", "L"],
    ["黔阳", "126", "101251301", "Q"],
    ["邵阳", "126", "101250901", "S"],
    ["湘潭", "126", "101250201", "X"],
    ["益阳", "126", "101250701", "Y"],
    ["岳阳", "126", "101251001", "Y"],
    ["永州", "126", "101251401", "Y"],
    ["株洲", "126", "101250301", "Z"],
    ["张家界", "126", "101251101", "Z"],
    ["广东", "4", "127"],
    ["广州", "127", "101280101", "G"],
    ["潮州", "127", "101281501", "C"],
    ["东莞", "127", "101281601", "D"],
    ["佛山", "127", "101280800", "F"],
    ["惠州", "127", "101280301", "H"],
    ["河源", "127", "101281201", "H"],
    ["江门", "127", "101281101", "J"],
    ["揭阳", "127", "101281901", "J"],
    ["梅州", "127", "101280401", "M"],
    ["茂名", "127", "101282001", "M"],
    ["清远", "127", "101281301", "Q"],
    ["韶关", "127", "101280201", "S"],
    ["汕头", "127", "101280501", "S"],
    ["深圳", "127", "101280601", "S"],
    ["汕尾", "127", "101282101", "S"],
    ["云浮", "127", "101281401", "Y"],
    ["阳江", "127", "101281801", "Y"],
    ["珠海", "127", "101280701", "Z"],
    ["肇庆", "127", "101280901", "Z"],
    ["湛江", "127", "101281001", "Z"],
    ["中山", "127", "101281701", "Z"],
    ["广西", "4", "128"],
    ["南宁", "128", "101300101", "N"],
    ["北海", "128", "101301301", "B"],
    ["百色", "128", "101301001", "B"],
    ["崇左", "128", "101300201", "C"],
    ["防城港", "128", "101301401", "F"],
    ["桂林", "128", "101300501", "G"],
    ["贵港", "128", "101300801", "G"],
    ["贺州", "128", "101300701", "H"],
    ["河池", "128", "101301201", "H"],
    ["柳州", "128", "101300301", "L"],
    ["来宾", "128", "101300401", "L"],
    ["钦州", "128", "101301101", "Q"],
    ["梧州", "128", "101300601", "W"],
    ["玉林", "128", "101300901", "Y"],
    ["海南", "4", "129"],
    ["海口", "129", "101310101", "H"],
    ["白沙", "129", "101310207", "B"],
    ["保亭", "129", "101310214", "B"],
    ["澄迈", "129", "101310204", "C"],
    ["昌江", "129", "101310206", "C"],
    ["东方", "129", "101310202", "D"],
    ["儋州", "129", "101310205", "D"],
    ["定安", "129", "101310209", "D"],
    ["临高", "129", "101310203", "L"],
    ["陵水", "129", "101310216", "L"],
    ["乐东", "129", "101310221", "L"],
    ["琼山", "129", "101310102", "Q"],
    ["琼中", "129", "101310208", "Q"],
    ["琼海", "129", "101310211", "Q"],
    ["清兰", "129", "101310213", "Q"],
    ["南沙岛", "129", "101310220", "N"],
    ["三亚", "129", "101310201", "S"],
    ["珊瑚岛", "129", "101310218", "S"],
    ["屯昌", "129", "101310210", "T"],
    ["通什", "129", "101310222", "T"],
    ["文昌", "129", "101310212", "W"],
    ["万宁", "129", "101310215", "W"],
    ["西沙", "129", "101310217", "X"],
    ["永署礁", "129", "101310219", "Y"],
    ["西北地区", "0", "5"],
    ["陕西", "5", "130"],
    ["西安", "130", "101110101", "X"],
    ["安康", "130", "101110701", "A"],
    ["宝鸡", "130", "101110901", "B"],
    ["汉中", "130", "101110801", "H"],
    ["商洛", "130", "101110601", "S"],
    ["铜川", "130", "101111001", "T"],
    ["渭南", "130", "101110501", "W"],
    ["咸阳", "130", "101110200", "X"],
    ["延安", "130", "101110300", "Y"],
    ["榆林", "130", "101110401", "Y"],
    ["甘肃", "5", "131"],
    ["兰州", "131", "101160101", "L"],
    ["白银", "131", "101161301", "B"],
    ["定西", "131", "101160201", "D"],
    ["合作", "131", "101161201", "H"],
    ["金昌", "131", "101160601", "J"],
    ["酒泉", "131", "101160801", "J"],
    ["临夏", "131", "101161101", "L"],
    ["平凉", "131", "101160301", "P"],
    ["庆阳", "131", "101160401", "Q"],
    ["天水", "131", "101160901", "T"],
    ["武威", "131", "101160501", "W"],
    ["武都", "131", "101161001", "W"],
    ["张掖", "131", "101160701", "Z"],
    ["宁夏", "5", "132"],
    ["银川", "132", "101170101", "Y"],
    ["固原", "132", "101170401", "G"],
    ["石嘴山", "132", "101170201", "S"],
    ["吴忠", "132", "101170301", "W"],
    ["中卫", "132", "101170501", "Z"],
    ["青海", "5", "133"],
    ["西宁", "133", "101150101", "X"],
    ["果洛", "133", "101150501", "G"],
    ["海西", "133", "101150701", "H"],
    ["海北", "133", "101150801", "H"],
    ["海东", "133", "101150201", "H"],
    ["黄南", "133", "101150301", "H"],
    ["海南", "133", "101150401", "H"],
    ["玉树", "133", "101150601", "Y"],
    ["新疆", "5", "134"],
    ["乌鲁木齐", "134", "101130101", "W"],
    ["阿勒泰", "134", "101131401", "A"],
    ["阿图什", "134", "101131501", "A"],
    ["阿克苏", "134", "101130801", "A"],
    ["阿拉尔", "134", "101130701", "A"],
    ["博乐", "134", "1011301601", "B"],
    ["昌吉", "134", "101130401", "C"],
    ["哈密", "134", "101131201", "H"],
    ["和田", "134", "101131301", "H"],
    ["克拉玛依", "134", "101130201", "K"],
    ["喀什", "134", "101130901", "K"],
    ["库尔勒", "134", "101130601", "K"],
    ["石河子", "134", "101130301", "S"],
    ["吐鲁番", "134", "101130501", "T"],
    ["塔城", "134", "101131101", "T"],
    ["伊宁", "134", "101131001", "Y"],
    ["西南地区", "0", "6"],
    ["重庆", "6", "135"],
    ["重庆", "135", "101040100", "C"],
    ["四川", "6", "136"],
    ["成都", "136", "101270101", "C"],
    ["阿坝", "136", "101271901", "A"],
    ["巴中", "136", "101270901", "B"],
    ["德阳", "136", "101272001", "D"],
    ["达州", "136", "101270601", "D"],
    ["广元", "136", "101272101", "G"],
    ["甘孜", "136", "101271801", "G"],
    ["泸州", "136", "101271001", "L"],
    ["乐山", "136", "101271401", "L"],
    ["凉山", "136", "101271601", "L"],
    ["眉山", "136", "101271501", "M"],
    ["绵阳", "136", "101270401", "M"],
    ["南充", "136", "101270501", "N"],
    ["内江", "136", "101271201", "N"],
    ["攀枝花", "136", "101270201", "P"],
    ["遂宁", "136", "101270701", "S"],
    ["广安", "136", "101270801", "G"],
    ["雅安", "136", "101271701", "Y"],
    ["宜宾", "136", "101271101", "Y"],
    ["资阳", "136", "101271301", "Z"],
    ["自贡", "136", "101270301", "Z"],
    ["贵州", "6", "137"],
    ["贵阳", "137", "101260101", "G"],
    ["安顺", "137", "101260301", "A"],
    ["毕节", "137", "101260701", "B"],
    ["都匀", "137", "101260401", "D"],
    ["凯里", "137", "101260501", "K"],
    ["六盘水", "137", "101260801", "L"],
    ["铜仁", "137", "101260601", "T"],
    ["遵义", "137", "101260201", "Z"],
    ["黔西", "137", "101260901", "Q"],
    ["云南", "6", "138"],
    ["昆明", "138", "101290101", "K"],
    ["保山", "138", "101290501", "B"],
    ["楚雄", "138", "101290801", "C"],
    ["大理", "138", "101290201", "D"],
    ["德宏", "138", "101291501", "D"],
    ["红河", "138", "101290301", "H"],
    ["景洪", "138", "101291601", "J"],
    ["临沧", "138", "101291101", "L"],
    ["丽江", "138", "101291401", "L"],
    ["怒江", "138", "101291201", "N"],
    ["曲靖", "138", "101290401", "Q"],
    ["思茅", "138", "101290901", "S"],
    ["文山", "138", "101290601", "W"],
    ["玉溪", "138", "101290701", "Y"],
    ["昭通", "138", "101291001", "Z"],
    ["中甸", "138", "101291301", "Z"],
    ["西藏", "6", "139"],
    ["拉萨", "139", "101140101", "L"],
    ["阿里", "139", "101140701", "A"],
    ["昌都", "139", "101140501", "C"],
    ["林芝", "139", "101140401", "L"],
    ["那曲", "139", "101140601", "N"],
    ["日喀则", "139", "101140201", "R"],
    ["山南", "139", "101140301", "S"],
    ["港澳台", "0", "7"],
    ["香港", "7", "140"],
    ["香港", "140", "101320101", "X"],
    ["澳门", "7", "141"],
    ["澳门", "141", "101330101", "A"],
    ["台湾", "7", "142"],
    ["台北县", "142", "101340101", "T"],
    ["高雄", "142", "101340201", "G"],
    ["花莲", "142", "101341001", "H"],
    ["嘉义", "142", "101340901", "J"],
    ["马公", "142", "101340801", "M"],
    ["彭佳屿", "142", "101341201", "P"],
    ["台南", "142", "101340301", "T"],
    ["台中", "142", "101340401", "T"],
    ["桃园", "142", "101340501", "T"],
    ["台东", "142", "101341101", "T"],
    ["新竹县", "142", "101340601", "X"],
    ["宜兰", "142", "101340701", "Y"]
];
var Weather = (function () {
    var Private = {
        CityArr: CityArr,
        getProId: function (proName) {
            var ProId;
            for (var i = 0, len = Private.CityArr.length; i < len; ++i) {
                if (Private.CityArr[i][0] == proName) {
                    ProId = Private.CityArr[i][2];
                }
            }
            return ProId
        },
        getCityId: function (CityName) {
            var CityId;
            for (var i = 0, len = Private.CityArr.length; i < len; ++i) {
                if (Number(Private.CityArr[i][1]) > 100 && Private.CityArr[i][0] == CityName) {
                    CityId = Private.CityArr[i][2];
                    break;
                }
            }
            return CityId;
        },
        getWeather: function (city_list, callback) {
            if (!city_list) {
                return false;
            }
            cur_item = 0;
            var doAjaxFun = function (obj) {
                    cityid = Private.getCityId(obj);
                    if (cityid) {
                        if (!Public.full) {
                            url = "/static/weather/" + cityid + "_1.txt";
                        } else {
                            url = "/static/weather/" + cityid + ".txt";
                        }
                        $.ajax({
                            url: url,
                            success: function (text) {
                                eval(text);
                                tmp = typeof wetJSON == "undefined" && typeof weatherJSON != "undefined" ? weatherJSON : wetJSON;
                                if (tmp) {
                                    if (typeof wetJSON == "undefined") {
                                        wetJSON = null;
                                    }
                                    if (typeof weatherJSON == "undefined") {
                                        weatherJSON = null;
                                    }
                                    if (typeof tmp.temp == "object") {
                                        tmp.temp = tmp.temp.join("~");
                                    }
                                    if (tmp.temp) {
                                        tmp.temp = (tmp.temp).split('~');
                                    }
                                    Public.result.push(tmp);
                                    cur_item++;
                                    if (cur_item == city_list.length) {
                                        callback(Public.result);
                                        Public.result = [];
                                    } else {
                                        doAjaxFun(city_list[cur_item]);
                                    }
                                }
                            }
                        });
                    }
                }
            var len = city_list.length;
            if (len) {
                doAjaxFun(city_list[0]);
            }


        }
    };
    var Public = {
        get: function (city, callback) {
            if (city) {
                return Private.getWeather(city, callback);
            }
        },
        full: false,
        result: []
    };
    return Public;
})();