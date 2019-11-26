local stringUtil = require('stringUtil')

local filesWoker = {
    getLogicalDrives = function()
        return stringUtil.split(GetLogicalDrives(), ',')
    end,
    getDirectories = function(path)
        return stringUtil.split(GetDirectories(path), ',')
    end,
    getFiles = function (path)
        return stringUtil.split(GetFiles(path), ',')
    end
}

return filesWoker
