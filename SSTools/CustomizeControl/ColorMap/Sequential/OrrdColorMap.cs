﻿using System.Drawing;
namespace SSTools.ColorMap.Sequential
{
	/// <summary>
	/// Orrdカラーマップ
	/// </summary>
	public class OrrdColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrrdColorMap()
		{
			colorMap = orrd_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] orrd_map_ =
		{
			Color.FromArgb(255,247,236),		// 0
			Color.FromArgb(254,246,234),		// 1
			Color.FromArgb(254,246,233),		// 2
			Color.FromArgb(254,245,232),		// 3
			Color.FromArgb(254,245,231),		// 4
			Color.FromArgb(254,244,230),		// 5
			Color.FromArgb(254,244,229),		// 6
			Color.FromArgb(254,243,228),		// 7
			Color.FromArgb(254,243,226),		// 8
			Color.FromArgb(254,242,225),		// 9
			Color.FromArgb(254,242,224),		// 10
			Color.FromArgb(254,241,223),		// 11
			Color.FromArgb(254,241,222),		// 12
			Color.FromArgb(254,240,221),		// 13
			Color.FromArgb(254,240,220),		// 14
			Color.FromArgb(254,239,219),		// 15
			Color.FromArgb(254,239,217),		// 16
			Color.FromArgb(254,239,216),		// 17
			Color.FromArgb(254,238,215),		// 18
			Color.FromArgb(254,238,214),		// 19
			Color.FromArgb(254,237,213),		// 20
			Color.FromArgb(254,237,212),		// 21
			Color.FromArgb(254,236,211),		// 22
			Color.FromArgb(254,236,210),		// 23
			Color.FromArgb(254,235,208),		// 24
			Color.FromArgb(254,235,207),		// 25
			Color.FromArgb(254,234,206),		// 26
			Color.FromArgb(254,234,205),		// 27
			Color.FromArgb(254,233,204),		// 28
			Color.FromArgb(254,233,203),		// 29
			Color.FromArgb(254,232,202),		// 30
			Color.FromArgb(254,232,200),		// 31
			Color.FromArgb(253,231,199),		// 32
			Color.FromArgb(253,231,198),		// 33
			Color.FromArgb(253,230,197),		// 34
			Color.FromArgb(253,230,195),		// 35
			Color.FromArgb(253,229,194),		// 36
			Color.FromArgb(253,228,193),		// 37
			Color.FromArgb(253,228,191),		// 38
			Color.FromArgb(253,227,190),		// 39
			Color.FromArgb(253,226,189),		// 40
			Color.FromArgb(253,226,187),		// 41
			Color.FromArgb(253,225,186),		// 42
			Color.FromArgb(253,225,185),		// 43
			Color.FromArgb(253,224,184),		// 44
			Color.FromArgb(253,223,182),		// 45
			Color.FromArgb(253,223,181),		// 46
			Color.FromArgb(253,222,180),		// 47
			Color.FromArgb(253,221,178),		// 48
			Color.FromArgb(253,221,177),		// 49
			Color.FromArgb(253,220,176),		// 50
			Color.FromArgb(253,220,174),		// 51
			Color.FromArgb(253,219,173),		// 52
			Color.FromArgb(253,218,172),		// 53
			Color.FromArgb(253,218,170),		// 54
			Color.FromArgb(253,217,169),		// 55
			Color.FromArgb(253,216,168),		// 56
			Color.FromArgb(253,216,166),		// 57
			Color.FromArgb(253,215,165),		// 58
			Color.FromArgb(253,214,164),		// 59
			Color.FromArgb(253,214,162),		// 60
			Color.FromArgb(253,213,161),		// 61
			Color.FromArgb(253,213,160),		// 62
			Color.FromArgb(253,212,158),		// 63
			Color.FromArgb(253,211,157),		// 64
			Color.FromArgb(253,211,156),		// 65
			Color.FromArgb(253,210,156),		// 66
			Color.FromArgb(253,209,155),		// 67
			Color.FromArgb(253,208,154),		// 68
			Color.FromArgb(253,207,153),		// 69
			Color.FromArgb(253,207,152),		// 70
			Color.FromArgb(253,206,152),		// 71
			Color.FromArgb(253,205,151),		// 72
			Color.FromArgb(253,204,150),		// 73
			Color.FromArgb(253,203,149),		// 74
			Color.FromArgb(253,203,148),		// 75
			Color.FromArgb(253,202,148),		// 76
			Color.FromArgb(253,201,147),		// 77
			Color.FromArgb(253,200,146),		// 78
			Color.FromArgb(253,200,145),		// 79
			Color.FromArgb(253,199,144),		// 80
			Color.FromArgb(253,198,143),		// 81
			Color.FromArgb(253,197,143),		// 82
			Color.FromArgb(253,196,142),		// 83
			Color.FromArgb(253,196,141),		// 84
			Color.FromArgb(253,195,140),		// 85
			Color.FromArgb(253,194,139),		// 86
			Color.FromArgb(253,193,139),		// 87
			Color.FromArgb(253,192,138),		// 88
			Color.FromArgb(253,192,137),		// 89
			Color.FromArgb(253,191,136),		// 90
			Color.FromArgb(253,190,135),		// 91
			Color.FromArgb(253,189,134),		// 92
			Color.FromArgb(253,189,134),		// 93
			Color.FromArgb(253,188,133),		// 94
			Color.FromArgb(253,187,132),		// 95
			Color.FromArgb(252,186,131),		// 96
			Color.FromArgb(252,185,130),		// 97
			Color.FromArgb(252,183,128),		// 98
			Color.FromArgb(252,182,127),		// 99
			Color.FromArgb(252,180,126),		// 100
			Color.FromArgb(252,179,124),		// 101
			Color.FromArgb(252,177,123),		// 102
			Color.FromArgb(252,176,122),		// 103
			Color.FromArgb(252,174,120),		// 104
			Color.FromArgb(252,173,119),		// 105
			Color.FromArgb(252,172,118),		// 106
			Color.FromArgb(252,170,116),		// 107
			Color.FromArgb(252,169,115),		// 108
			Color.FromArgb(252,167,113),		// 109
			Color.FromArgb(252,166,112),		// 110
			Color.FromArgb(252,164,111),		// 111
			Color.FromArgb(252,163,109),		// 112
			Color.FromArgb(252,161,108),		// 113
			Color.FromArgb(252,160,107),		// 114
			Color.FromArgb(252,159,105),		// 115
			Color.FromArgb(252,157,104),		// 116
			Color.FromArgb(252,156,103),		// 117
			Color.FromArgb(252,154,101),		// 118
			Color.FromArgb(252,153,100),		// 119
			Color.FromArgb(252,151,99),		// 120
			Color.FromArgb(252,150,97),		// 121
			Color.FromArgb(252,148,96),		// 122
			Color.FromArgb(252,147,95),		// 123
			Color.FromArgb(252,146,93),		// 124
			Color.FromArgb(252,144,92),		// 125
			Color.FromArgb(252,143,91),		// 126
			Color.FromArgb(252,141,89),		// 127
			Color.FromArgb(251,140,88),		// 128
			Color.FromArgb(251,139,88),		// 129
			Color.FromArgb(250,137,87),		// 130
			Color.FromArgb(250,136,87),		// 131
			Color.FromArgb(250,135,86),		// 132
			Color.FromArgb(249,134,86),		// 133
			Color.FromArgb(249,132,85),		// 134
			Color.FromArgb(248,131,85),		// 135
			Color.FromArgb(248,130,84),		// 136
			Color.FromArgb(248,129,83),		// 137
			Color.FromArgb(247,127,83),		// 138
			Color.FromArgb(247,126,82),		// 139
			Color.FromArgb(246,125,82),		// 140
			Color.FromArgb(246,124,81),		// 141
			Color.FromArgb(246,122,81),		// 142
			Color.FromArgb(245,121,80),		// 143
			Color.FromArgb(245,120,80),		// 144
			Color.FromArgb(244,119,79),		// 145
			Color.FromArgb(244,117,79),		// 146
			Color.FromArgb(244,116,78),		// 147
			Color.FromArgb(243,115,78),		// 148
			Color.FromArgb(243,114,77),		// 149
			Color.FromArgb(242,112,77),		// 150
			Color.FromArgb(242,111,76),		// 151
			Color.FromArgb(242,110,75),		// 152
			Color.FromArgb(241,109,75),		// 153
			Color.FromArgb(241,107,74),		// 154
			Color.FromArgb(240,106,74),		// 155
			Color.FromArgb(240,105,73),		// 156
			Color.FromArgb(239,103,73),		// 157
			Color.FromArgb(239,102,72),		// 158
			Color.FromArgb(239,101,72),		// 159
			Color.FromArgb(238,99,71),		// 160
			Color.FromArgb(237,98,69),		// 161
			Color.FromArgb(237,96,68),		// 162
			Color.FromArgb(236,94,67),		// 163
			Color.FromArgb(235,93,66),		// 164
			Color.FromArgb(234,91,64),		// 165
			Color.FromArgb(234,89,63),		// 166
			Color.FromArgb(233,88,62),		// 167
			Color.FromArgb(232,86,60),		// 168
			Color.FromArgb(231,84,59),		// 169
			Color.FromArgb(231,83,58),		// 170
			Color.FromArgb(230,81,57),		// 171
			Color.FromArgb(229,80,55),		// 172
			Color.FromArgb(228,78,54),		// 173
			Color.FromArgb(227,76,53),		// 174
			Color.FromArgb(227,75,51),		// 175
			Color.FromArgb(226,73,50),		// 176
			Color.FromArgb(225,71,49),		// 177
			Color.FromArgb(224,70,48),		// 178
			Color.FromArgb(224,68,46),		// 179
			Color.FromArgb(223,66,45),		// 180
			Color.FromArgb(222,65,44),		// 181
			Color.FromArgb(221,63,42),		// 182
			Color.FromArgb(221,61,41),		// 183
			Color.FromArgb(220,60,40),		// 184
			Color.FromArgb(219,58,39),		// 185
			Color.FromArgb(218,56,37),		// 186
			Color.FromArgb(218,55,36),		// 187
			Color.FromArgb(217,53,35),		// 188
			Color.FromArgb(216,51,33),		// 189
			Color.FromArgb(215,50,32),		// 190
			Color.FromArgb(215,48,31),		// 191
			Color.FromArgb(214,46,30),		// 192
			Color.FromArgb(213,45,29),		// 193
			Color.FromArgb(211,43,28),		// 194
			Color.FromArgb(210,42,27),		// 195
			Color.FromArgb(209,40,26),		// 196
			Color.FromArgb(208,39,25),		// 197
			Color.FromArgb(207,37,24),		// 198
			Color.FromArgb(206,36,23),		// 199
			Color.FromArgb(205,34,22),		// 200
			Color.FromArgb(203,33,21),		// 201
			Color.FromArgb(202,31,20),		// 202
			Color.FromArgb(201,30,19),		// 203
			Color.FromArgb(200,28,18),		// 204
			Color.FromArgb(199,27,17),		// 205
			Color.FromArgb(198,25,16),		// 206
			Color.FromArgb(197,24,15),		// 207
			Color.FromArgb(196,22,14),		// 208
			Color.FromArgb(194,21,13),		// 209
			Color.FromArgb(193,19,12),		// 210
			Color.FromArgb(192,18,11),		// 211
			Color.FromArgb(191,16,10),		// 212
			Color.FromArgb(190,15,9),		// 213
			Color.FromArgb(189,13,8),		// 214
			Color.FromArgb(188,12,7),		// 215
			Color.FromArgb(187,10,6),		// 216
			Color.FromArgb(185,9,5),		// 217
			Color.FromArgb(184,7,4),		// 218
			Color.FromArgb(183,6,4),		// 219
			Color.FromArgb(182,4,3),		// 220
			Color.FromArgb(181,3,2),		// 221
			Color.FromArgb(180,1,1),		// 222
			Color.FromArgb(179,0,0),		// 223
			Color.FromArgb(177,0,0),		// 224
			Color.FromArgb(175,0,0),		// 225
			Color.FromArgb(174,0,0),		// 226
			Color.FromArgb(172,0,0),		// 227
			Color.FromArgb(171,0,0),		// 228
			Color.FromArgb(169,0,0),		// 229
			Color.FromArgb(167,0,0),		// 230
			Color.FromArgb(166,0,0),		// 231
			Color.FromArgb(164,0,0),		// 232
			Color.FromArgb(162,0,0),		// 233
			Color.FromArgb(161,0,0),		// 234
			Color.FromArgb(159,0,0),		// 235
			Color.FromArgb(157,0,0),		// 236
			Color.FromArgb(156,0,0),		// 237
			Color.FromArgb(154,0,0),		// 238
			Color.FromArgb(153,0,0),		// 239
			Color.FromArgb(151,0,0),		// 240
			Color.FromArgb(149,0,0),		// 241
			Color.FromArgb(148,0,0),		// 242
			Color.FromArgb(146,0,0),		// 243
			Color.FromArgb(144,0,0),		// 244
			Color.FromArgb(143,0,0),		// 245
			Color.FromArgb(141,0,0),		// 246
			Color.FromArgb(140,0,0),		// 247
			Color.FromArgb(138,0,0),		// 248
			Color.FromArgb(136,0,0),		// 249
			Color.FromArgb(135,0,0),		// 250
			Color.FromArgb(133,0,0),		// 251
			Color.FromArgb(131,0,0),		// 252
			Color.FromArgb(130,0,0),		// 253
			Color.FromArgb(128,0,0),		// 254
			Color.FromArgb(127,0,0),		// 255
		};
	}
}
