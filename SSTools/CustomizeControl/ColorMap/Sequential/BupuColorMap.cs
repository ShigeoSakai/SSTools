﻿using System.Drawing;
namespace SSTools.ColorMap.Sequential
{
	/// <summary>
	/// Bupuカラーマップ
	/// </summary>
	public class BupuColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BupuColorMap()
		{
			colorMap = bupu_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] bupu_map_ =
		{
			Color.FromArgb(247,252,253),		// 0
			Color.FromArgb(246,251,252),		// 1
			Color.FromArgb(245,250,252),		// 2
			Color.FromArgb(244,250,252),		// 3
			Color.FromArgb(244,249,251),		// 4
			Color.FromArgb(243,249,251),		// 5
			Color.FromArgb(242,248,251),		// 6
			Color.FromArgb(241,248,251),		// 7
			Color.FromArgb(241,247,250),		// 8
			Color.FromArgb(240,247,250),		// 9
			Color.FromArgb(239,246,250),		// 10
			Color.FromArgb(239,246,249),		// 11
			Color.FromArgb(238,245,249),		// 12
			Color.FromArgb(237,245,249),		// 13
			Color.FromArgb(236,244,249),		// 14
			Color.FromArgb(236,244,248),		// 15
			Color.FromArgb(235,243,248),		// 16
			Color.FromArgb(234,243,248),		// 17
			Color.FromArgb(234,242,247),		// 18
			Color.FromArgb(233,242,247),		// 19
			Color.FromArgb(232,241,247),		// 20
			Color.FromArgb(231,241,247),		// 21
			Color.FromArgb(231,240,246),		// 22
			Color.FromArgb(230,240,246),		// 23
			Color.FromArgb(229,239,246),		// 24
			Color.FromArgb(228,239,245),		// 25
			Color.FromArgb(228,238,245),		// 26
			Color.FromArgb(227,238,245),		// 27
			Color.FromArgb(226,237,245),		// 28
			Color.FromArgb(226,237,244),		// 29
			Color.FromArgb(225,236,244),		// 30
			Color.FromArgb(224,236,244),		// 31
			Color.FromArgb(223,235,243),		// 32
			Color.FromArgb(222,235,243),		// 33
			Color.FromArgb(221,234,243),		// 34
			Color.FromArgb(220,233,242),		// 35
			Color.FromArgb(219,232,242),		// 36
			Color.FromArgb(218,231,241),		// 37
			Color.FromArgb(217,231,241),		// 38
			Color.FromArgb(216,230,240),		// 39
			Color.FromArgb(215,229,240),		// 40
			Color.FromArgb(214,228,239),		// 41
			Color.FromArgb(213,228,239),		// 42
			Color.FromArgb(212,227,239),		// 43
			Color.FromArgb(211,226,238),		// 44
			Color.FromArgb(210,225,238),		// 45
			Color.FromArgb(209,224,237),		// 46
			Color.FromArgb(208,224,237),		// 47
			Color.FromArgb(207,223,236),		// 48
			Color.FromArgb(206,222,236),		// 49
			Color.FromArgb(205,221,236),		// 50
			Color.FromArgb(204,221,235),		// 51
			Color.FromArgb(203,220,235),		// 52
			Color.FromArgb(202,219,234),		// 53
			Color.FromArgb(201,218,234),		// 54
			Color.FromArgb(200,217,233),		// 55
			Color.FromArgb(199,217,233),		// 56
			Color.FromArgb(197,216,232),		// 57
			Color.FromArgb(196,215,232),		// 58
			Color.FromArgb(195,214,232),		// 59
			Color.FromArgb(194,213,231),		// 60
			Color.FromArgb(193,213,231),		// 61
			Color.FromArgb(192,212,230),		// 62
			Color.FromArgb(191,211,230),		// 63
			Color.FromArgb(190,210,229),		// 64
			Color.FromArgb(189,210,229),		// 65
			Color.FromArgb(188,209,229),		// 66
			Color.FromArgb(187,208,228),		// 67
			Color.FromArgb(186,207,228),		// 68
			Color.FromArgb(185,207,228),		// 69
			Color.FromArgb(184,206,227),		// 70
			Color.FromArgb(183,205,227),		// 71
			Color.FromArgb(182,205,226),		// 72
			Color.FromArgb(181,204,226),		// 73
			Color.FromArgb(180,203,226),		// 74
			Color.FromArgb(179,202,225),		// 75
			Color.FromArgb(178,202,225),		// 76
			Color.FromArgb(177,201,225),		// 77
			Color.FromArgb(176,200,224),		// 78
			Color.FromArgb(175,199,224),		// 79
			Color.FromArgb(174,199,223),		// 80
			Color.FromArgb(173,198,223),		// 81
			Color.FromArgb(172,197,223),		// 82
			Color.FromArgb(171,197,222),		// 83
			Color.FromArgb(170,196,222),		// 84
			Color.FromArgb(169,195,222),		// 85
			Color.FromArgb(167,194,221),		// 86
			Color.FromArgb(166,194,221),		// 87
			Color.FromArgb(165,193,220),		// 88
			Color.FromArgb(164,192,220),		// 89
			Color.FromArgb(163,192,220),		// 90
			Color.FromArgb(162,191,219),		// 91
			Color.FromArgb(161,190,219),		// 92
			Color.FromArgb(160,189,218),		// 93
			Color.FromArgb(159,189,218),		// 94
			Color.FromArgb(158,188,218),		// 95
			Color.FromArgb(157,187,217),		// 96
			Color.FromArgb(157,186,217),		// 97
			Color.FromArgb(156,185,216),		// 98
			Color.FromArgb(156,183,215),		// 99
			Color.FromArgb(155,182,215),		// 100
			Color.FromArgb(154,181,214),		// 101
			Color.FromArgb(154,180,214),		// 102
			Color.FromArgb(153,179,213),		// 103
			Color.FromArgb(153,178,212),		// 104
			Color.FromArgb(152,176,212),		// 105
			Color.FromArgb(152,175,211),		// 106
			Color.FromArgb(151,174,210),		// 107
			Color.FromArgb(151,173,210),		// 108
			Color.FromArgb(150,172,209),		// 109
			Color.FromArgb(149,170,208),		// 110
			Color.FromArgb(149,169,208),		// 111
			Color.FromArgb(148,168,207),		// 112
			Color.FromArgb(148,167,207),		// 113
			Color.FromArgb(147,166,206),		// 114
			Color.FromArgb(147,164,205),		// 115
			Color.FromArgb(146,163,205),		// 116
			Color.FromArgb(145,162,204),		// 117
			Color.FromArgb(145,161,203),		// 118
			Color.FromArgb(144,160,203),		// 119
			Color.FromArgb(144,158,202),		// 120
			Color.FromArgb(143,157,202),		// 121
			Color.FromArgb(143,156,201),		// 122
			Color.FromArgb(142,155,200),		// 123
			Color.FromArgb(141,154,200),		// 124
			Color.FromArgb(141,152,199),		// 125
			Color.FromArgb(140,151,198),		// 126
			Color.FromArgb(140,150,198),		// 127
			Color.FromArgb(140,149,197),		// 128
			Color.FromArgb(140,147,197),		// 129
			Color.FromArgb(140,146,196),		// 130
			Color.FromArgb(140,145,195),		// 131
			Color.FromArgb(140,143,195),		// 132
			Color.FromArgb(140,142,194),		// 133
			Color.FromArgb(140,141,193),		// 134
			Color.FromArgb(140,139,193),		// 135
			Color.FromArgb(140,138,192),		// 136
			Color.FromArgb(140,137,191),		// 137
			Color.FromArgb(140,135,191),		// 138
			Color.FromArgb(140,134,190),		// 139
			Color.FromArgb(140,133,189),		// 140
			Color.FromArgb(140,131,189),		// 141
			Color.FromArgb(140,130,188),		// 142
			Color.FromArgb(140,129,187),		// 143
			Color.FromArgb(140,127,187),		// 144
			Color.FromArgb(140,126,186),		// 145
			Color.FromArgb(140,125,185),		// 146
			Color.FromArgb(140,123,185),		// 147
			Color.FromArgb(140,122,184),		// 148
			Color.FromArgb(140,120,183),		// 149
			Color.FromArgb(140,119,183),		// 150
			Color.FromArgb(140,118,182),		// 151
			Color.FromArgb(140,116,181),		// 152
			Color.FromArgb(140,115,181),		// 153
			Color.FromArgb(140,114,180),		// 154
			Color.FromArgb(140,112,179),		// 155
			Color.FromArgb(140,111,179),		// 156
			Color.FromArgb(140,110,178),		// 157
			Color.FromArgb(140,108,177),		// 158
			Color.FromArgb(140,107,177),		// 159
			Color.FromArgb(139,106,176),		// 160
			Color.FromArgb(139,104,175),		// 161
			Color.FromArgb(139,103,175),		// 162
			Color.FromArgb(139,102,174),		// 163
			Color.FromArgb(139,100,174),		// 164
			Color.FromArgb(139,99,173),		// 165
			Color.FromArgb(139,98,172),		// 166
			Color.FromArgb(139,96,172),		// 167
			Color.FromArgb(138,95,171),		// 168
			Color.FromArgb(138,94,170),		// 169
			Color.FromArgb(138,93,170),		// 170
			Color.FromArgb(138,91,169),		// 171
			Color.FromArgb(138,90,169),		// 172
			Color.FromArgb(138,89,168),		// 173
			Color.FromArgb(138,87,167),		// 174
			Color.FromArgb(138,86,167),		// 175
			Color.FromArgb(137,85,166),		// 176
			Color.FromArgb(137,83,165),		// 177
			Color.FromArgb(137,82,165),		// 178
			Color.FromArgb(137,81,164),		// 179
			Color.FromArgb(137,79,164),		// 180
			Color.FromArgb(137,78,163),		// 181
			Color.FromArgb(137,77,162),		// 182
			Color.FromArgb(137,75,162),		// 183
			Color.FromArgb(136,74,161),		// 184
			Color.FromArgb(136,73,160),		// 185
			Color.FromArgb(136,71,160),		// 186
			Color.FromArgb(136,70,159),		// 187
			Color.FromArgb(136,69,159),		// 188
			Color.FromArgb(136,67,158),		// 189
			Color.FromArgb(136,66,157),		// 190
			Color.FromArgb(136,65,157),		// 191
			Color.FromArgb(135,63,156),		// 192
			Color.FromArgb(135,62,155),		// 193
			Color.FromArgb(135,60,154),		// 194
			Color.FromArgb(135,59,153),		// 195
			Color.FromArgb(134,57,152),		// 196
			Color.FromArgb(134,55,151),		// 197
			Color.FromArgb(134,54,150),		// 198
			Color.FromArgb(134,52,148),		// 199
			Color.FromArgb(134,51,147),		// 200
			Color.FromArgb(133,49,146),		// 201
			Color.FromArgb(133,48,145),		// 202
			Color.FromArgb(133,46,144),		// 203
			Color.FromArgb(133,44,143),		// 204
			Color.FromArgb(132,43,142),		// 205
			Color.FromArgb(132,41,141),		// 206
			Color.FromArgb(132,40,140),		// 207
			Color.FromArgb(132,38,139),		// 208
			Color.FromArgb(132,37,138),		// 209
			Color.FromArgb(131,35,137),		// 210
			Color.FromArgb(131,34,136),		// 211
			Color.FromArgb(131,32,135),		// 212
			Color.FromArgb(131,30,134),		// 213
			Color.FromArgb(131,29,133),		// 214
			Color.FromArgb(130,27,132),		// 215
			Color.FromArgb(130,26,131),		// 216
			Color.FromArgb(130,24,130),		// 217
			Color.FromArgb(130,23,129),		// 218
			Color.FromArgb(129,21,128),		// 219
			Color.FromArgb(129,19,127),		// 220
			Color.FromArgb(129,18,126),		// 221
			Color.FromArgb(129,16,125),		// 222
			Color.FromArgb(129,15,124),		// 223
			Color.FromArgb(127,14,122),		// 224
			Color.FromArgb(125,14,121),		// 225
			Color.FromArgb(124,13,119),		// 226
			Color.FromArgb(122,13,118),		// 227
			Color.FromArgb(121,12,116),		// 228
			Color.FromArgb(119,12,114),		// 229
			Color.FromArgb(117,11,113),		// 230
			Color.FromArgb(116,11,111),		// 231
			Color.FromArgb(114,10,110),		// 232
			Color.FromArgb(112,10,108),		// 233
			Color.FromArgb(111,9,107),		// 234
			Color.FromArgb(109,9,105),		// 235
			Color.FromArgb(107,8,104),		// 236
			Color.FromArgb(106,8,102),		// 237
			Color.FromArgb(104,8,101),		// 238
			Color.FromArgb(103,7,99),		// 239
			Color.FromArgb(101,7,98),		// 240
			Color.FromArgb(99,6,96),		// 241
			Color.FromArgb(98,6,94),		// 242
			Color.FromArgb(96,5,93),		// 243
			Color.FromArgb(94,5,91),		// 244
			Color.FromArgb(93,4,90),		// 245
			Color.FromArgb(91,4,88),		// 246
			Color.FromArgb(90,3,87),		// 247
			Color.FromArgb(88,3,85),		// 248
			Color.FromArgb(86,2,84),		// 249
			Color.FromArgb(85,2,82),		// 250
			Color.FromArgb(83,1,81),		// 251
			Color.FromArgb(81,1,79),		// 252
			Color.FromArgb(80,0,78),		// 253
			Color.FromArgb(78,0,76),		// 254
			Color.FromArgb(77,0,75),		// 255
		};
	}
}
