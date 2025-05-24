from utils import Utils

class Constants:
    CAFE_ROOT = None
    COMMON_DEV_KEY = None
    COMMON_KEY = None
    DEBUG = False

    APP_VERSION = "2.0.0-beta1"

    WORKING_UNITY_VERSIONS = ["2017.4.40f1"]

    @staticmethod
    def initialize():
        Constants.CAFE_ROOT = Utils.getCafeRoot()
        Constants.COMMON_DEV_KEY = Utils.getCommonDevKey()
        Constants.COMMON_KEY = Utils.getCommonKey()