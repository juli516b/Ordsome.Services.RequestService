﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Ordsome.Services.CrossCuttingConcerns.Languages
{
    public class ListOfLanguages
    {
        private const string json = @"{ 'list': [
    {'id':1,'code':'ab','name':'Abkhaz','nativeName':'аҧсуа'},
    {'id':2,'code':'aa','name':'Afar','nativeName':'Afaraf'},
    {'id':3,'code':'af','name':'Afrikaans','nativeName':'Afrikaans'},
    {'id':4,'code':'ak','name':'Akan','nativeName':'Akan'},
    {'id':5,'code':'sq','name':'Albanian','nativeName':'Shqip'},
    {'id':6,'code':'am','name':'Amharic','nativeName':'አማርኛ'},
    {'id':7,'code':'ar','name':'Arabic','nativeName':'العربية'},
    {'id':8,'code':'an','name':'Aragonese','nativeName':'Aragonés'},
    {'id':9,'code':'hy','name':'Armenian','nativeName':'Հայերեն'},
    {'id':10,'code':'as','name':'Assamese','nativeName':'অসমীয়া'},
    {'id':11,'code':'av','name':'Avaric','nativeName':'авар мацӀ, магӀарул мацӀ'},
    {'id':12,'code':'ae','name':'Avestan','nativeName':'avesta'},
    {'id':13,'code':'ay','name':'Aymara','nativeName':'aymar aru'},
    {'id':14,'code':'az','name':'Azerbaijani','nativeName':'azərbaycan dili'},
    {'id':15,'code':'bm','name':'Bambara','nativeName':'bamanankan'},
    {'id':16,'code':'ba','name':'Bashkir','nativeName':'башҡорт теле'},
    {'id':17,'code':'eu','name':'Basque','nativeName':'euskara, euskera'},
    {'id':18,'code':'be','name':'Belarusian','nativeName':'Беларуская'},
    {'id':19,'code':'bn','name':'Bengali','nativeName':'বাংলা'},
    {'id':20,'code':'bh','name':'Bihari','nativeName':'भोजपुरी'},
    {'id':21,'code':'bi','name':'Bislama','nativeName':'Bislama'},
    {'id':22,'code':'bs','name':'Bosnian','nativeName':'bosanski jezik'},
    {'id':23,'code':'br','name':'Breton','nativeName':'brezhoneg'},
    {'id':24,'code':'bg','name':'Bulgarian','nativeName':'български език'},
    {'id':25,'code':'my','name':'Burmese','nativeName':'ဗမာစာ'},
    {'id':26,'code':'ca','name':'Catalan; Valencian','nativeName':'Català'},
    {'id':27,'code':'ch','name':'Chamorro','nativeName':'Chamoru'},
    {'id':28,'code':'ce','name':'Chechen','nativeName':'нохчийн мотт'},
    {'id':29,'code':'ny','name':'Chichewa; Chewa; Nyanja','nativeName':'chiCheŵa, chinyanja'},
    {'id':30,'code':'zh','name':'Chinese','nativeName':'中文 (Zhōngwén), 汉语, 漢語'},
    {'id':31,'code':'cv','name':'Chuvash','nativeName':'чӑваш чӗлхи'},
    {'id':32,'code':'kw','name':'Cornish','nativeName':'Kernewek'},
    {'id':33,'code':'co','name':'Corsican','nativeName':'corsu, lingua corsa'},
    {'id':34,'code':'cr','name':'Cree','nativeName':'ᓀᐦᐃᔭᐍᐏᐣ'},
    {'id':35,'code':'hr','name':'Croatian','nativeName':'hrvatski'},
    {'id':36,'code':'cs','name':'Czech','nativeName':'česky, čeština'},
    {'id':37,'code':'dk','name':'Danish','nativeName':'dansk'},
    {'id':38,'code':'dv','name':'Divehi; Dhivehi; Maldivian;','nativeName':'ދިވެހި'},
    {'id':39,'code':'nl','name':'Dutch','nativeName':'Nederlands, Vlaams'},
    {'id':40,'code':'en','name':'English','nativeName':'English'},
    {'id':41,'code':'eo','name':'Esperanto','nativeName':'Esperanto'},
    {'id':42,'code':'et','name':'Estonian','nativeName':'eesti, eesti keel'},
    {'id':43,'code':'ee','name':'Ewe','nativeName':'Eʋegbe'},
    {'id':44,'code':'fo','name':'Faroese','nativeName':'føroyskt'},
    {'id':45,'code':'fj','name':'Fijian','nativeName':'vosa Vakaviti'},
    {'id':46,'code':'fi','name':'Finnish','nativeName':'suomi, suomen kieli'},
    {'id':47,'code':'fr','name':'French','nativeName':'français, langue française'},
    {'id':48,'code':'ff','name':'Fula; Fulah; Pulaar; Pular','nativeName':'Fulfulde, Pulaar, Pular'},
    {'id':49,'code':'gl','name':'Galician','nativeName':'Galego'},
    {'id':50,'code':'ka','name':'Georgian','nativeName':'ქართული'},
    {'id':51,'code':'de','name':'German','nativeName':'Deutsch'},
    {'id':52,'code':'el','name':'Greek, Modern','nativeName':'Ελληνικά'},
    {'id':53,'code':'gn','name':'Guaraní','nativeName':'Avañeẽ'},
    {'id':54,'code':'gu','name':'Gujarati','nativeName':'ગુજરાતી'},
    {'id':55,'code':'ht','name':'Haitian; Haitian Creole','nativeName':'Kreyòl ayisyen'},
    {'id':56,'code':'ha','name':'Hausa','nativeName':'Hausa, هَوُسَ'},
    {'id':57,'code':'he','name':'Hebrew (modern)','nativeName':'עברית'},
    {'id':58,'code':'hz','name':'Herero','nativeName':'Otjiherero'},
    {'id':59,'code':'hi','name':'Hindi','nativeName':'हिन्दी, हिंदी'},
    {'id':60,'code':'ho','name':'Hiri Motu','nativeName':'Hiri Motu'},
    {'id':61,'code':'hu','name':'Hungarian','nativeName':'Magyar'},
    {'id':62,'code':'ia','name':'Interlingua','nativeName':'Interlingua'},
    {'id':63,'code':'id','name':'Indonesian','nativeName':'Bahasa Indonesia'},
    {'id':64,'code':'ie','name':'Interlingue','nativeName':'Originally called Occidental; then Interlingue after WWII'},
    {'id':65,'code':'ga','name':'Irish','nativeName':'Gaeilge'},
    {'id':66,'code':'ig','name':'Igbo','nativeName':'Asụsụ Igbo'},
    {'id':67,'code':'ik','name':'Inupiaq','nativeName':'Iñupiaq, Iñupiatun'},
    {'id':68,'code':'io','name':'Ido','nativeName':'Ido'},
    {'id':69,'code':'is','name':'Icelandic','nativeName':'Íslenska'},
    {'id':70,'code':'it','name':'Italian','nativeName':'Italiano'},
    {'id':71,'code':'iu','name':'Inuktitut','nativeName':'ᐃᓄᒃᑎᑐᑦ'},
    {'id':72,'code':'ja','name':'Japanese','nativeName':'日本語 (にほんご／にっぽんご)'},
    {'id':73,'code':'jv','name':'Javanese','nativeName':'basa Jawa'},
    {'id':74,'code':'kl','name':'Kalaallisut, Greenlandic','nativeName':'kalaallisut, kalaallit oqaasii'},
    {'id':75,'code':'kn','name':'Kannada','nativeName':'ಕನ್ನಡ'},
    {'id':76,'code':'kr','name':'Kanuri','nativeName':'Kanuri'},
    {'id':77,'code':'ks','name':'Kashmiri','nativeName':'कश्मीरी, كشميري‎'},
    {'id':78,'code':'kk','name':'Kazakh','nativeName':'Қазақ тілі'},
    {'id':79,'code':'km','name':'Khmer','nativeName':'ភាសាខ្មែរ'},
    {'id':80,'code':'ki','name':'Kikuyu, Gikuyu','nativeName':'Gĩkũyũ'},
    {'id':81,'code':'rw','name':'Kinyarwanda','nativeName':'Ikinyarwanda'},
    {'id':82,'code':'ky','name':'Kirghiz, Kyrgyz','nativeName':'кыргыз тили'},
    {'id':83,'code':'kv','name':'Komi','nativeName':'коми кыв'},
    {'id':84,'code':'kg','name':'Kongo','nativeName':'KiKongo'},
    {'id':85,'code':'ko','name':'Korean','nativeName':'한국어 (韓國語), 조선말 (朝鮮語)'},
    {'id':86,'code':'ku','name':'Kurdish','nativeName':'Kurdî, كوردی‎'},
    {'id':87,'code':'kj','name':'Kwanyama, Kuanyama','nativeName':'Kuanyama'},
    {'id':88,'code':'la','name':'Latin','nativeName':'latine, lingua latina'},
    {'id':89,'code':'lb','name':'Luxembourgish, Letzeburgesch','nativeName':'Lëtzebuergesch'},
    {'id':90,'code':'lg','name':'Luganda','nativeName':'Luganda'},
    {'id':91,'code':'li','name':'Limburgish, Limburgan, Limburger','nativeName':'Limburgs'},
    {'id':92,'code':'ln','name':'Lingala','nativeName':'Lingála'},
    {'id':93,'code':'lo','name':'Lao','nativeName':'ພາສາລາວ'},
    {'id':94,'code':'lt','name':'Lithuanian','nativeName':'lietuvių kalba'},
    {'id':95,'code':'lu','name':'Luba-Katanga','nativeName':''},
    {'id':96,'code':'lv','name':'Latvian','nativeName':'latviešu valoda'},
    {'id':97,'code':'gv','name':'Manx','nativeName':'Gaelg, Gailck'},
    {'id':98,'code':'mk','name':'Macedonian','nativeName':'македонски јазик'},
    {'id':99,'code':'mg','name':'Malagasy','nativeName':'Malagasy fiteny'},
    {'id':100,'code':'ms','name':'Malay','nativeName':'bahasa Melayu, بهاس ملايو‎'},
    {'id':101,'code':'ml','name':'Malayalam','nativeName':'മലയാളം'},
    {'id':102,'code':'mt','name':'Maltese','nativeName':'Malti'},
    {'id':103,'code':'mi','name':'Māori','nativeName':'te reo Māori'},
    {'id':104,'code':'mr','name':'Marathi (Marāṭhī)','nativeName':'मराठी'},
    {'id':105,'code':'mh','name':'Marshallese','nativeName':'Kajin M̧ajeļ'},
    {'id':106,'code':'mn','name':'Mongolian','nativeName':'монгол'},
    {'id':107,'code':'na','name':'Nauru','nativeName':'Ekakairũ Naoero'},
    {'id':108,'code':'nv','name':'Navajo, Navaho','nativeName':'Diné bizaad, Dinékʼehǰí'},
    {'id':109,'code':'nb','name':'Norwegian Bokmål','nativeName':'Norsk bokmål'},
    {'id':110,'code':'nd','name':'North Ndebele','nativeName':'isiNdebele'},
    {'id':111,'code':'ne','name':'Nepali','nativeName':'नेपाली'},
    {'id':112,'code':'ng','name':'Ndonga','nativeName':'Owambo'},
    {'id':113,'code':'nn','name':'Norwegian Nynorsk','nativeName':'Norsk nynorsk'},
    {'id':114,'code':'no','name':'Norwegian','nativeName':'Norsk'},
    {'id':115,'code':'ii','name':'Nuosu','nativeName':'ꆈꌠ꒿ Nuosuhxop'},
    {'id':116,'code':'nr','name':'South Ndebele','nativeName':'isiNdebele'},
    {'id':117,'code':'oc','name':'Occitan','nativeName':'Occitan'},
    {'id':118,'code':'oj','name':'Ojibwe, Ojibwa','nativeName':'ᐊᓂᔑᓈᐯᒧᐎᓐ'},
    {'id':119,'code':'cu','name':'Old Church Slavonic, Church Slavic, Church Slavonic, Old Bulgarian, Old Slavonic','nativeName':'ѩзыкъ словѣньскъ'},
    {'id':120,'code':'om','name':'Oromo','nativeName':'Afaan Oromoo'},
    {'id':121,'code':'or','name':'Oriya','nativeName':'ଓଡ଼ିଆ'},
    {'id':122,'code':'os','name':'Ossetian, Ossetic','nativeName':'ирон æвзаг'},
    {'id':123,'code':'pa','name':'Panjabi, Punjabi','nativeName':'ਪੰਜਾਬੀ, پنجابی‎'},
    {'id':124,'code':'pi','name':'Pāli','nativeName':'पाऴि'},
    {'id':125,'code':'fa','name':'Persian','nativeName':'فارسی'},
    {'id':126,'code':'pl','name':'Polish','nativeName':'polski'},
    {'id':127,'code':'ps','name':'Pashto, Pushto','nativeName':'پښتو'},
    {'id':128,'code':'pt','name':'Portuguese','nativeName':'Português'},
    {'id':129,'code':'qu','name':'Quechua','nativeName':'Runa Simi, Kichwa'},
    {'id':130,'code':'rm','name':'Romansh','nativeName':'rumantsch grischun'},
    {'id':131,'code':'rn','name':'Kirundi','nativeName':'kiRundi'},
    {'id':132,'code':'ro','name':'Romanian, Moldavian, Moldovan','nativeName':'română'},
    {'id':133,'code':'ru','name':'Russian','nativeName':'русский язык'},
    {'id':134,'code':'sa','name':'Sanskrit (Saṁskṛta)','nativeName':'संस्कृतम्'},
    {'id':135,'code':'sc','name':'Sardinian','nativeName':'sardu'},
    {'id':136,'code':'sd','name':'Sindhi','nativeName':'सिन्धी, سنڌي، سندھی‎'},
    {'id':137,'code':'se','name':'Northern Sami','nativeName':'Davvisámegiella'},
    {'id':138,'code':'sm','name':'Samoan','nativeName':'gagana faa Samoa'},
    {'id':139,'code':'sg','name':'Sango','nativeName':'yângâ tî sängö'},
    {'id':140,'code':'sr','name':'Serbian','nativeName':'српски језик'},
    {'id':141,'code':'gd','name':'Scottish Gaelic; Gaelic','nativeName':'Gàidhlig'},
    {'id':142,'code':'sn','name':'Shona','nativeName':'chiShona'},
    {'id':143,'code':'si','name':'Sinhala, Sinhalese','nativeName':'සිංහල'},
    {'id':144,'code':'sk','name':'Slovak','nativeName':'slovenčina'},
    {'id':145,'code':'sl','name':'Slovene','nativeName':'slovenščina'},
    {'id':146,'code':'so','name':'Somali','nativeName':'Soomaaliga, af Soomaali'},
    {'id':147,'code':'st','name':'Southern Sotho','nativeName':'Sesotho'},
    {'id':148,'code':'es','name':'Spanish; Castilian','nativeName':'español, castellano'},
    {'id':149,'code':'su','name':'Sundanese','nativeName':'Basa Sunda'},
    {'id':150,'code':'sw','name':'Swahili','nativeName':'Kiswahili'},
    {'id':151,'code':'ss','name':'Swati','nativeName':'SiSwati'},
    {'id':152,'code':'sv','name':'Swedish','nativeName':'svenska'},
    {'id':153,'code':'ta','name':'Tamil','nativeName':'தமிழ்'},
    {'id':154,'code':'te','name':'Telugu','nativeName':'తెలుగు'},
    {'id':155,'code':'tg','name':'Tajik','nativeName':'тоҷикӣ, toğikī, تاجیکی‎'},
    {'id':156,'code':'th','name':'Thai','nativeName':'ไทย'},
    {'id':157,'code':'ti','name':'Tigrinya','nativeName':'ትግርኛ'},
    {'id':158,'code':'bo','name':'Tibetan Standard, Tibetan, Central','nativeName':'བོད་ཡིག'},
    {'id':159,'code':'tk','name':'Turkmen','nativeName':'Türkmen, Түркмен'},
    {'id':161,'code':'tl','name':'Tagalog','nativeName':'Wikang Tagalog, ᜏᜒᜃᜅ᜔ ᜆᜄᜎᜓᜄ᜔'},
    {'id':162,'code':'tn','name':'Tswana','nativeName':'Setswana'},
    {'id':163,'code':'to','name':'Tonga (Tonga Islands)','nativeName':'faka Tonga'},
    {'id':164,'code':'tr','name':'Turkish','nativeName':'Türkçe'},
    {'id':165,'code':'ts','name':'Tsonga','nativeName':'Xitsonga'},
    {'id':166,'code':'tt','name':'Tatar','nativeName':'татарча, tatarça, تاتارچا‎'},
    {'id':167,'code':'tw','name':'Twi','nativeName':'Twi'},
    {'id':168,'code':'ty','name':'Tahitian','nativeName':'Reo Tahiti'},
    {'id':169,'code':'ug','name':'Uighur, Uyghur','nativeName':'Uyƣurqə, ئۇيغۇرچە‎'},
    {'id':170,'code':'uk','name':'Ukrainian','nativeName':'українська'},
    {'id':171,'code':'ur','name':'Urdu','nativeName':'اردو'},
    {'id':172,'code':'uz','name':'Uzbek','nativeName':'zbek, Ўзбек, أۇزبېك‎'},
    {'id':173,'code':'ve','name':'Venda','nativeName':'Tshivenḓa'},
    {'id':174,'code':'vi','name':'Vietnamese','nativeName':'Tiếng Việt'},
    {'id':175,'code':'vo','name':'Volapük','nativeName':'Volapük'},
    {'id':176,'code':'wa','name':'Walloon','nativeName':'Walon'},
    {'id':177,'code':'cy','name':'Welsh','nativeName':'Cymraeg'},
    {'id':178,'code':'wo','name':'Wolof','nativeName':'Wollof'},
    {'id':179,'code':'fy','name':'Western Frisian','nativeName':'Frysk'},
    {'id':180,'code':'xh','name':'Xhosa','nativeName':'isiXhosa'},
    {'id':181,'code':'yi','name':'Yiddish','nativeName':'ייִדיש'},
    {'id':182,'code':'yo','name':'Yoruba','nativeName':'Yorùbá'},
    {'id':183,'code':'za','name':'Zhuang, Chuang','nativeName':'Saɯ cueŋƅ, Saw cuengh'}]}";

        //TODO - parse the json once
//        private Dictionary<string, object> languagesByCode = JsonConvert.DeserializeObject<string, object>(json)

        public LanguageDto GetLanguageById(int id)
        {
            var o = JObject.Parse(json);
            var a = (JArray) o["list"];
            var _list = a.ToObject<IList<LanguageDto>>();

            return _list.FirstOrDefault(x => x.Id == id);
        }

        public LanguageDto GetLanguageByCode(string code)
        {
            var o = JObject.Parse(json);
            var a = (JArray) o["list"];
            var _list = a.ToObject<IList<LanguageDto>>();

            return _list.FirstOrDefault(x => x.LanguageCode == code);
        }

        public LanguageDto GetLanguage(Func<LanguageDto, bool> isMatch)
        {
            var o = JObject.Parse(json);
            var a = (JArray) o["list"];
            var _list = a.ToObject<IList<LanguageDto>>();

            return _list.FirstOrDefault(isMatch);
        }

        public IList<LanguageDto> GetList()
        {
            var o = JObject.Parse(json);
            var a = (JArray) o["list"];
            return a.ToObject<IList<LanguageDto>>();
        }
    }
}