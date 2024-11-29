﻿using System.Drawing;
namespace SSTools.ColorMap.Sequential
{
	/// <summary>
	/// Purdカラーマップ
	/// </summary>
	public class PurdColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PurdColorMap()
		{
			colorMap = purd_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] purd_map_ =
		{
			Color.FromArgb(247,244,249),		// 0
			Color.FromArgb(246,243,248),		// 1
			Color.FromArgb(245,242,248),		// 2
			Color.FromArgb(245,242,248),		// 3
			Color.FromArgb(244,241,247),		// 4
			Color.FromArgb(244,241,247),		// 5
			Color.FromArgb(243,240,247),		// 6
			Color.FromArgb(243,239,246),		// 7
			Color.FromArgb(242,239,246),		// 8
			Color.FromArgb(242,238,246),		// 9
			Color.FromArgb(241,238,245),		// 10
			Color.FromArgb(241,237,245),		// 11
			Color.FromArgb(240,236,245),		// 12
			Color.FromArgb(240,236,244),		// 13
			Color.FromArgb(239,235,244),		// 14
			Color.FromArgb(239,235,244),		// 15
			Color.FromArgb(238,234,243),		// 16
			Color.FromArgb(238,233,243),		// 17
			Color.FromArgb(237,233,243),		// 18
			Color.FromArgb(237,232,243),		// 19
			Color.FromArgb(236,232,242),		// 20
			Color.FromArgb(236,231,242),		// 21
			Color.FromArgb(235,230,242),		// 22
			Color.FromArgb(235,230,241),		// 23
			Color.FromArgb(234,229,241),		// 24
			Color.FromArgb(234,229,241),		// 25
			Color.FromArgb(233,228,240),		// 26
			Color.FromArgb(233,227,240),		// 27
			Color.FromArgb(232,227,240),		// 28
			Color.FromArgb(232,226,239),		// 29
			Color.FromArgb(231,226,239),		// 30
			Color.FromArgb(231,225,239),		// 31
			Color.FromArgb(230,224,238),		// 32
			Color.FromArgb(230,223,238),		// 33
			Color.FromArgb(229,222,237),		// 34
			Color.FromArgb(229,221,236),		// 35
			Color.FromArgb(228,219,236),		// 36
			Color.FromArgb(227,218,235),		// 37
			Color.FromArgb(227,217,234),		// 38
			Color.FromArgb(226,216,234),		// 39
			Color.FromArgb(226,214,233),		// 40
			Color.FromArgb(225,213,232),		// 41
			Color.FromArgb(224,212,232),		// 42
			Color.FromArgb(224,211,231),		// 43
			Color.FromArgb(223,209,231),		// 44
			Color.FromArgb(223,208,230),		// 45
			Color.FromArgb(222,207,229),		// 46
			Color.FromArgb(221,206,229),		// 47
			Color.FromArgb(221,204,228),		// 48
			Color.FromArgb(220,203,227),		// 49
			Color.FromArgb(220,202,227),		// 50
			Color.FromArgb(219,201,226),		// 51
			Color.FromArgb(219,199,225),		// 52
			Color.FromArgb(218,198,225),		// 53
			Color.FromArgb(217,197,224),		// 54
			Color.FromArgb(217,195,223),		// 55
			Color.FromArgb(216,194,223),		// 56
			Color.FromArgb(216,193,222),		// 57
			Color.FromArgb(215,192,221),		// 58
			Color.FromArgb(214,190,221),		// 59
			Color.FromArgb(214,189,220),		// 60
			Color.FromArgb(213,188,219),		// 61
			Color.FromArgb(213,187,219),		// 62
			Color.FromArgb(212,185,218),		// 63
			Color.FromArgb(211,184,217),		// 64
			Color.FromArgb(211,183,217),		// 65
			Color.FromArgb(211,182,216),		// 66
			Color.FromArgb(210,181,216),		// 67
			Color.FromArgb(210,180,215),		// 68
			Color.FromArgb(210,178,214),		// 69
			Color.FromArgb(209,177,214),		// 70
			Color.FromArgb(209,176,213),		// 71
			Color.FromArgb(209,175,213),		// 72
			Color.FromArgb(208,174,212),		// 73
			Color.FromArgb(208,173,211),		// 74
			Color.FromArgb(208,171,211),		// 75
			Color.FromArgb(207,170,210),		// 76
			Color.FromArgb(207,169,210),		// 77
			Color.FromArgb(207,168,209),		// 78
			Color.FromArgb(206,167,208),		// 79
			Color.FromArgb(206,166,208),		// 80
			Color.FromArgb(206,164,207),		// 81
			Color.FromArgb(205,163,207),		// 82
			Color.FromArgb(205,162,206),		// 83
			Color.FromArgb(205,161,205),		// 84
			Color.FromArgb(204,160,205),		// 85
			Color.FromArgb(204,159,204),		// 86
			Color.FromArgb(203,158,204),		// 87
			Color.FromArgb(203,156,203),		// 88
			Color.FromArgb(203,155,202),		// 89
			Color.FromArgb(202,154,202),		// 90
			Color.FromArgb(202,153,201),		// 91
			Color.FromArgb(202,152,201),		// 92
			Color.FromArgb(201,151,200),		// 93
			Color.FromArgb(201,149,199),		// 94
			Color.FromArgb(201,148,199),		// 95
			Color.FromArgb(201,147,198),		// 96
			Color.FromArgb(201,145,198),		// 97
			Color.FromArgb(202,144,197),		// 98
			Color.FromArgb(203,143,196),		// 99
			Color.FromArgb(204,141,195),		// 100
			Color.FromArgb(204,140,195),		// 101
			Color.FromArgb(205,138,194),		// 102
			Color.FromArgb(206,137,193),		// 103
			Color.FromArgb(206,135,192),		// 104
			Color.FromArgb(207,134,192),		// 105
			Color.FromArgb(208,132,191),		// 106
			Color.FromArgb(208,131,190),		// 107
			Color.FromArgb(209,129,190),		// 108
			Color.FromArgb(210,128,189),		// 109
			Color.FromArgb(210,126,188),		// 110
			Color.FromArgb(211,125,187),		// 111
			Color.FromArgb(212,123,187),		// 112
			Color.FromArgb(212,122,186),		// 113
			Color.FromArgb(213,120,185),		// 114
			Color.FromArgb(214,119,185),		// 115
			Color.FromArgb(215,117,184),		// 116
			Color.FromArgb(215,116,183),		// 117
			Color.FromArgb(216,115,182),		// 118
			Color.FromArgb(217,113,182),		// 119
			Color.FromArgb(217,112,181),		// 120
			Color.FromArgb(218,110,180),		// 121
			Color.FromArgb(219,109,179),		// 122
			Color.FromArgb(219,107,179),		// 123
			Color.FromArgb(220,106,178),		// 124
			Color.FromArgb(221,104,177),		// 125
			Color.FromArgb(221,103,177),		// 126
			Color.FromArgb(222,101,176),		// 127
			Color.FromArgb(223,100,175),		// 128
			Color.FromArgb(223,98,174),		// 129
			Color.FromArgb(223,96,173),		// 130
			Color.FromArgb(223,94,171),		// 131
			Color.FromArgb(224,92,170),		// 132
			Color.FromArgb(224,90,169),		// 133
			Color.FromArgb(224,88,168),		// 134
			Color.FromArgb(224,86,167),		// 135
			Color.FromArgb(225,85,165),		// 136
			Color.FromArgb(225,83,164),		// 137
			Color.FromArgb(225,81,163),		// 138
			Color.FromArgb(225,79,162),		// 139
			Color.FromArgb(226,77,161),		// 140
			Color.FromArgb(226,75,159),		// 141
			Color.FromArgb(226,73,158),		// 142
			Color.FromArgb(226,71,157),		// 143
			Color.FromArgb(227,69,156),		// 144
			Color.FromArgb(227,68,155),		// 145
			Color.FromArgb(227,66,153),		// 146
			Color.FromArgb(227,64,152),		// 147
			Color.FromArgb(228,62,151),		// 148
			Color.FromArgb(228,60,150),		// 149
			Color.FromArgb(228,58,149),		// 150
			Color.FromArgb(228,56,147),		// 151
			Color.FromArgb(229,54,146),		// 152
			Color.FromArgb(229,52,145),		// 153
			Color.FromArgb(229,51,144),		// 154
			Color.FromArgb(229,49,143),		// 155
			Color.FromArgb(230,47,142),		// 156
			Color.FromArgb(230,45,140),		// 157
			Color.FromArgb(230,43,139),		// 158
			Color.FromArgb(230,41,138),		// 159
			Color.FromArgb(230,40,136),		// 160
			Color.FromArgb(229,39,135),		// 161
			Color.FromArgb(228,39,133),		// 162
			Color.FromArgb(228,38,132),		// 163
			Color.FromArgb(227,37,130),		// 164
			Color.FromArgb(226,36,128),		// 165
			Color.FromArgb(225,36,127),		// 166
			Color.FromArgb(225,35,125),		// 167
			Color.FromArgb(224,34,123),		// 168
			Color.FromArgb(223,34,122),		// 169
			Color.FromArgb(222,33,120),		// 170
			Color.FromArgb(221,32,119),		// 171
			Color.FromArgb(221,31,117),		// 172
			Color.FromArgb(220,31,115),		// 173
			Color.FromArgb(219,30,114),		// 174
			Color.FromArgb(218,29,112),		// 175
			Color.FromArgb(217,29,110),		// 176
			Color.FromArgb(217,28,109),		// 177
			Color.FromArgb(216,27,107),		// 178
			Color.FromArgb(215,26,105),		// 179
			Color.FromArgb(214,26,104),		// 180
			Color.FromArgb(214,25,102),		// 181
			Color.FromArgb(213,24,101),		// 182
			Color.FromArgb(212,23,99),		// 183
			Color.FromArgb(211,23,97),		// 184
			Color.FromArgb(210,22,96),		// 185
			Color.FromArgb(210,21,94),		// 186
			Color.FromArgb(209,21,92),		// 187
			Color.FromArgb(208,20,91),		// 188
			Color.FromArgb(207,19,89),		// 189
			Color.FromArgb(206,18,88),		// 190
			Color.FromArgb(206,18,86),		// 191
			Color.FromArgb(204,17,85),		// 192
			Color.FromArgb(203,17,84),		// 193
			Color.FromArgb(201,16,84),		// 194
			Color.FromArgb(199,15,83),		// 195
			Color.FromArgb(197,15,83),		// 196
			Color.FromArgb(196,14,82),		// 197
			Color.FromArgb(194,14,81),		// 198
			Color.FromArgb(192,13,81),		// 199
			Color.FromArgb(191,13,80),		// 200
			Color.FromArgb(189,12,80),		// 201
			Color.FromArgb(187,11,79),		// 202
			Color.FromArgb(186,11,78),		// 203
			Color.FromArgb(184,10,78),		// 204
			Color.FromArgb(182,10,77),		// 205
			Color.FromArgb(181,9,77),		// 206
			Color.FromArgb(179,9,76),		// 207
			Color.FromArgb(177,8,76),		// 208
			Color.FromArgb(175,7,75),		// 209
			Color.FromArgb(174,7,74),		// 210
			Color.FromArgb(172,6,74),		// 211
			Color.FromArgb(170,6,73),		// 212
			Color.FromArgb(169,5,73),		// 213
			Color.FromArgb(167,5,72),		// 214
			Color.FromArgb(165,4,71),		// 215
			Color.FromArgb(164,4,71),		// 216
			Color.FromArgb(162,3,70),		// 217
			Color.FromArgb(160,2,70),		// 218
			Color.FromArgb(158,2,69),		// 219
			Color.FromArgb(157,1,68),		// 220
			Color.FromArgb(155,1,68),		// 221
			Color.FromArgb(153,0,67),		// 222
			Color.FromArgb(152,0,67),		// 223
			Color.FromArgb(150,0,66),		// 224
			Color.FromArgb(149,0,64),		// 225
			Color.FromArgb(147,0,63),		// 226
			Color.FromArgb(146,0,62),		// 227
			Color.FromArgb(144,0,61),		// 228
			Color.FromArgb(142,0,60),		// 229
			Color.FromArgb(141,0,59),		// 230
			Color.FromArgb(139,0,58),		// 231
			Color.FromArgb(138,0,56),		// 232
			Color.FromArgb(136,0,55),		// 233
			Color.FromArgb(135,0,54),		// 234
			Color.FromArgb(133,0,53),		// 235
			Color.FromArgb(132,0,52),		// 236
			Color.FromArgb(130,0,51),		// 237
			Color.FromArgb(129,0,50),		// 238
			Color.FromArgb(127,0,49),		// 239
			Color.FromArgb(126,0,47),		// 240
			Color.FromArgb(124,0,46),		// 241
			Color.FromArgb(122,0,45),		// 242
			Color.FromArgb(121,0,44),		// 243
			Color.FromArgb(119,0,43),		// 244
			Color.FromArgb(118,0,42),		// 245
			Color.FromArgb(116,0,41),		// 246
			Color.FromArgb(115,0,40),		// 247
			Color.FromArgb(113,0,38),		// 248
			Color.FromArgb(112,0,37),		// 249
			Color.FromArgb(110,0,36),		// 250
			Color.FromArgb(109,0,35),		// 251
			Color.FromArgb(107,0,34),		// 252
			Color.FromArgb(106,0,33),		// 253
			Color.FromArgb(104,0,32),		// 254
			Color.FromArgb(103,0,31),		// 255
		};
	}
}
