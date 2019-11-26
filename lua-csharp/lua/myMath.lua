local myMath = {
    value = 13,
    add = function(num1, num2)
        return num1 + num2
    end,
    sub = function(num1, num2)
        return num1 - num2
    end,
    mul = function(num1, num2)
        return num1 * num2
    end,
    div = function(num1, num2)
        return num1 / num2
    end
}

myMath.value = 123;

return myMath
