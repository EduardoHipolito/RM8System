export function toUTF8Array(str) {
    var utf8 = [];
    for (var i = 0; i < str.length; i++) {
        var charcode = str.charCodeAt(i);
        if (charcode < 0x80) utf8.push(charcode);
        else if (charcode < 0x800) {
            utf8.push(0xc0 | (charcode >> 6),
                0x80 | (charcode & 0x3f));
        }
        else if (charcode < 0xd800 || charcode >= 0xe000) {
            utf8.push(0xe0 | (charcode >> 12),
                0x80 | ((charcode >> 6) & 0x3f),
                0x80 | (charcode & 0x3f));
        }
        else {
            i++;
            charcode = 0x10000 + (((charcode & 0x3ff) << 10)
                | (str.charCodeAt(i) & 0x3ff));
            utf8.push(0xf0 | (charcode >> 18),
                0x80 | ((charcode >> 12) & 0x3f),
                0x80 | ((charcode >> 6) & 0x3f),
                0x80 | (charcode & 0x3f));
        }
    }
    return utf8;
}

export function Utf8ArrayToStr(array) {
    var out, i, len, c;
    var char2, char3;

    out = "";
    len = array.length;
    i = 0;
    while (i < len) {
        c = array[i++];
        switch (c >> 4) {
            case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
                // 0xxxxxxx
                out += String.fromCharCode(c);
                break;
            case 12: case 13:
                // 110x xxxx   10xx xxxx
                char2 = array[i++];
                out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
                break;
            case 14:
                // 1110 xxxx  10xx xxxx  10xx xxxx
                char2 = array[i++];
                char3 = array[i++];
                out += String.fromCharCode(((c & 0x0F) << 12) |
                    ((char2 & 0x3F) << 6) |
                    ((char3 & 0x3F) << 0));
                break;
        }
    }
    console.log(out);
    return out;
}

export function encode_utf8(str) {
    var bytes = [];
    for (var i = 0; i < str.length; i++) {
        var char = str.charCodeAt(i);
        // You can combine both these calls into one,
        //    bytes.push(char >>> 8, char & 0xff);
        bytes.push(char >>> 8);
        bytes.push(char & 0xFF);
    }
    console.log(bytes);
    return bytes;
}

export function decode_utf8(bytes) {
    var str = "";
    // You could make it faster by reading bytes.length once.
    for (var i = 0; i < bytes.length; i += 2) {
        // If you're using signed bytes, you probably need to mask here.
        var char = bytes[i] << 8;
        // (undefined | 0) === 0 so you can save a test here by doing
        //     var char = (bytes[i] << 8) | (bytes[i + 1] & 0xff);
        if (bytes[i + 1])
            char |= bytes[i + 1];
        // Instead of using string += you could push char onto an array
        // and take advantage of the fact that String.fromCharCode can
        // take any number of arguments to do
        //     String.fromCharCode.apply(null, chars);
        str += String.fromCharCode(char);
    }
    console.log(str);
    return str;
}