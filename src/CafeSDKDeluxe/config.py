from typing import Any

def from_str(x: Any) -> str:
    assert isinstance(x, str)
    return x

def from_bool(x: Any) -> bool:
    assert isinstance(x, bool)
    return x

class ConfigData:
    unity_path: str
    export_path: str
    auto_export: bool = False

    def __init__(self, unity_path: str = "", export_path: str = "", auto_export: bool = False) -> None:
        self.unity_path = unity_path
        self.export_path = export_path
        self.auto_export = auto_export

    @staticmethod
    def from_dict(obj: Any) -> 'ConfigData':
        assert isinstance(obj, dict)
        unity_path = from_str(obj.get("unityPath"))
        export_path = from_str(obj.get("exportPath"))
        auto_export = from_bool(obj.get("autoExport", False))
        return ConfigData(unity_path, export_path, auto_export)

    def to_dict(self) -> dict:
        result: dict = {}
        result["unityPath"] = from_str(self.unity_path)
        result["exportPath"] = from_str(self.export_path)
        result["autoExport"] = from_bool(self.auto_export)
        return result