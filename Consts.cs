using System;
using System.Collections.Generic;

namespace ListMaster
{
    public enum BrowserType
    {
        // *новый браузер* имя должно совпадать с названием процесса
        chrome,
        iexplore,
        firefox
    }

    public class Consts
    {
        // *новый браузер*
        public static Dictionary<BrowserType, string> BrowserDriver = new Dictionary<BrowserType, string>()
        {
            {BrowserType.chrome, "chromedriver" },
            {BrowserType.iexplore, "IEDriverServer" },
            {BrowserType.firefox, "geckodriver" },
        };

        // UI
        //form
        public const string MESSAGE_NO_SCRIPT = "Не выбран сценарий";
        public const string MESSAGE_CONFIG_ERROR = "Ошибка файла конфигурации";
        public const string MESSAGE_WORKING = "Работаю...";
        public const string MESSAGE_STOPING = "Останавливаю...";
        public const string MESSAGE_STOPED = "Остановлено";
        public const string MESSAGE_NO_BROWSER = "Браузер не запущен";
        public const string MESSAGE_NO_FILE = "Файл не выбран";
        public const string MESSAGE_WORK_FINISHED = "Работа программы завершена";
        public const string MESSAGE_NO_OPERATOR = "Оператор не назначен";
        public const string CHOOSE_SCRIPT = "Выбрать сценарий";
        public const string CHOOSE_FILE_TITLE = "Выбор файла со списком";
        public const string CHOOSE_FILE_EXCEL_TYPES = "Excel files (*.xlsx;*.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
        public const string MESSAGE_SETTINGS_SAVED = "Настройки сохранены";
        public const string PATH_FIREFOX_EXE = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";
        
        public const string MESSAGE_TRY_CONNECT = "Попытка соединения";

        //operator
        public const string MESSAGE_YES = "да";
        public const string MESSAGE_NO = "нет";
        public const string MESSAGE_NO_DATA = "нет сведений";
        public const string MESSAGE_WRONG_FORMAT = "Пустая строка или неверный формат данных";
        public const string MESSAGE_RECOVERY = "Восстановливаю работу";
        public const string MESSAGE_DONT_KNOW_CURRENT_PAGE = "Я потерялась. Отведите меня на форму поиска";
        public const string MESSAGE_NO_ACTUAL_DOCS = "Действительных документов не найдено";
        public const string MESSAGE_NO_ACTUAL_REGISTRATION = "Нет действующей регистрации";
        public const string MESSAGE_SAME_ADDRESS = "Адрес не изменился";
        public const string MESSAGE_NO_ENTRIES = "Записей не найдено";
        public const string MESSAGE_NO_ADDRESSES_IN_PPO = "Нет адресов в Досье";
        public const string MESSAGE_NO_IN_ASRP = "Нет в АСРП";
        public const string MESSAGE_NO_IN_FORM_TEN = "Нет в АСР (Журнал Форм 10)";
        public const string MESSAGE_NO_ACTUAL_ADDRESSES_IN_FORM_TEN = "Нет действующей регистрации (Журнал форм 10)";

        //ERRORS
        public const string ERROR_CANT_GET_DATA = "Не удалось получить данные";
        public const string ERROR_CANT_GET_SEARCH_RESULT_TABLE = "Не удалось получить таблицу результатов поиска";
        public const string ERROR_NO_CONNECTION = "Не удается установить соединение с сервисом";
        public const string ERROR_LOGOUT = "Пользователь не аутентифицирован в СУДИС";
        public const string ERROR_LOGIN = "Ошибка аутентификации";
        public const string ERROR_NETWORK = "Network Error";
        public const string ERROR_UNEXPECTED_PAGE = "Неожиданная страница";
        public const string ERROR_LOST_BROWSER = "Потеряна связь с драйвером браузера";
        public const string ERROR_SERVER = "Ошибка сервера";
        public const string ERROR_LONG_TAB_OPEN = "Превышено время ожидания открытия вкладки";
        public const string ERROR_LONG_TAB_LOAD = "Превышено время ожидания загрузки вкладки";//Произошла непредвиденная ошибка
        public const string ERROR_UNEXPECTED_ERROR = "Произошла непредвиденная ошибка";
        public const string ERORR_SCROLL_KEYWORD = "scrolled";

        //WAITS
        public const int WAIT_60_SEC = 60;
        public const int WAIT_30_SEC = 30;
        public const int WAIT_3_SEC = 3;
        public const int WAIT_10_SEC = 10;
        public const int WAIT_1800_SEC = 1800;//30 минут


        //SCRIPTS *новый сценарий*
        public const string SCRIPT_PPO_ASR_FORM_10_REGISTRATION = "ППО.АСР.Форм 10.Регистрация";
        public const string SCRIPT_GISMU_ESFL_DOC = "ГИСМУ.ЕСФЛ.Документ";
        public const string SCRIPT_GISMU_ESFL_REG = "ГИСМУ.ЕСФЛ.Регистрация";
        public const string SCRIPT_GISMU_ESFL_NATION_BIRTHPLACE = "ГИСМУ.ЕСФЛ.Гражданство и место рождения";
        public const string SCRIPT_GISMU_RP_DOC_STATUS = "ГИСМУ.РП.Актуальный документ";
        public const string SCRIPT_GISMU_RP_FORM_ONE = "ГИСМУ.РП.Форма 1";

        //АРХИВНЫЕ:
        //public const string SCRIPT_ASRP_DOCUMENT = "АСРП.Досье.Документ";
        //public const string SCRIPT_ASRP_PASSPORTS_FORM_ONE = "АСРП.Паспортный учет.Форма 1";
        public const string SCRIPT_PPO_DOSSIER_REGISTRATION = "ППО.Досье.Регистрация";
        public const string SCRIPT_PPO_DOSSIER_DOCUMENT = "ППО.Досье.Документ";

        // BL
        public const int DEFAULT_TRY_RECOVERY_COUNT = 5;
        public const int DEFAULT_TRY_REPEAT_LOGIN_COUNT = 2;
        public const int SAVE_ENTRIES_COUNT = 10;
        public const int DEFAULT_START_ROW = 2;
        public const int MAX_FILENAME_LENGTH = 29;
        public const int CHILD_AGE_TOP_BORDER = 14;
        public const string NEW_ROW = "\r\n";
        
        public const char SEPARATOR_REGISTRATION = ';';
        public const char SEPARATOR_PERIOD = '-';
        public const string JSON_EXTENSION = ".json";
        public const string DOSSIER_DOC_STATUS_VALID = "Действительный";
        public const string DOC_STATUS_FOR_ISSUE = "На выдачу";
        public const string DOC_TYPE_PASSPORT_RF = "Паспорт гражданина Российской Федерации";
        public const string DOC_TYPE_PASSPORT_IG = "Иностранный паспорт";
        public const string DOC_TYPE_DEATH_CERT = "Свидетельство о смерти";
        public const string DOC_TYPE_SIGNAL = "Сигнал";
        public const string DEATH_SIGNAL = "Смерть или признание судом умершим";
        public const int WEB_TABLE_DOC_STATUS_COLUMN = 7;
        public const int WEB_TABLE_DOC_TYPE_COLUMN = 4;
        public const string FORM10_REGISTRATION_TYPE = "ПВР";
        

        public const string DOSSIER_REGISTRATION_TYPE_CONSTANT_RF = "Регистрация граждан РФ по месту жительства";
        public const string DOSSIER_REGISTRATION_TYPE_TEMP_RF = "Регистрация граждан РФ по месту пребывания в жилом помещении";
        public const string DOSSIER_REGISTRATION_TYPE_HOTEL_RF = "Регистрация граждан РФ месту пребывания в гостинице, санатории, доме отдыха и пр.";
        public const string DOSSIER_REGISTRATION_TYPE_CONSTANT_IG = "Регистрация ИГ по месту жительства";
        public const string DOSSIER_REGISTRATION_TYPE_TEMP_IG = "Регистрация ИГ по месту пребывания";
        public const string DOSSIER_REGISTRATION_TYPE_HOTEL_IG = "Регистрация ИГ по месту пребывания в гостинице, санатории, доме отдыха и пр.";
        public const string REG_ROLE_PERSON_ADDRESS = "Адрес лица";

        public const string ESFL_REG_TYPE_CONST = "Регистрация по месту жительства";
        public const string ESFL_REG_TYPE_TEMP = "Регистрация по месту пребывания";
        public const string ESFL_REG_UNTIL_NOW = "по настоящее время";
        public static DateTime ESFL_REG_UNTIL_NOW_DATE_EQUIVALENT = new DateTime(2999, 01, 01);
        
        public const string ESFL_REG_SOURCE_REGUCHET = "Регистрационный учёт";
        public const string ESFL_REG_SOURCE_MIGUCHET = "Миграционный учёт";
        public const int ESFL_REG_ADDRESSES_TABLE_COLUMNS = 6;

        public const string ASRP_DOC_STATUS_VALID = "ДЕЙСТВИТЕЛЕН";
        public const string DOSSIER_PAGINATOR_MAX_VALUE = "50";
        public const string NAME_PARSE_EXTRA_SYMBOL = "(";
        public const string NAME_PARSE_DELIMITER = " ";

        //PATTERNS
        public const string PATTERN_ESFL_DOC_TYPE = @"\D+";
        public const string PATTERN_ESFL_DOC_SERIES = @"\s\d{4}\s";
        public const string PATTERN_ESFL_DOC_NUMBER = @"\d{6}$";
        public const string PATTERN_ESFL_DEP_NAME = @"\D+$";
        public const string PATTERN_ESFL_DEP_CODE = @"\d{3}-\d{3}";
        

        public static Dictionary<string, string> CacheDict { get; set; } = new Dictionary<string, string>();
        
    }
}
