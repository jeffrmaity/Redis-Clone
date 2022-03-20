# Redis-Clone
A Small Redis Clone System

Supported Commands

SET <VARNAME> <VALUE>: Set VARNAME to VALUE
GET <VARNAME>: Retrieve and return value of VARNAME
INCR <VARNAME>: Increment VARNAME by 1
INCRBY <VARNAME> [COUNT]: Increment VARNAME by COUNT
DECR <VARNAME>: Decrement VARNAME by 1
DECRBY <VARNAME> [COUNT]: Decrement VARNAME by COUNT
RPUSH <VARNAME> <VALUE>: Push VALUE on the right of list VARNAME
RPOP <VARNAME>: POP right value from list VARNAME
LPUSH <VARNAME> <VALUE>: PUSH VALUE on the left of list VARNAME
LPOP <VARNAME>: POP left value from list VARNAME
LINDEX <VARNAME> <INDEX>: GET value at index INDEX from LIST VARNAME
EXPIRES <VARNAME> <TIME>: EXPIRE value of VARNAME after TIME seconds