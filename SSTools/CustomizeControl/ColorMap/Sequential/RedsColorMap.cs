﻿using System.Drawing;
namespace SSTools.ColorMap.Sequential
{
	/// <summary>
	/// Redsカラーマップ
	/// </summary>
	public class RedsColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RedsColorMap()
		{
			colorMap = reds_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] reds_map_ =
		{
			Color.FromArgb(255,245,240),		// 0
			Color.FromArgb(254,244,239),		// 1
			Color.FromArgb(254,243,238),		// 2
			Color.FromArgb(254,243,237),		// 3
			Color.FromArgb(254,242,236),		// 4
			Color.FromArgb(254,241,235),		// 5
			Color.FromArgb(254,241,234),		// 6
			Color.FromArgb(254,240,233),		// 7
			Color.FromArgb(254,239,232),		// 8
			Color.FromArgb(254,239,231),		// 9
			Color.FromArgb(254,238,230),		// 10
			Color.FromArgb(254,237,229),		// 11
			Color.FromArgb(254,237,228),		// 12
			Color.FromArgb(254,236,227),		// 13
			Color.FromArgb(254,235,226),		// 14
			Color.FromArgb(254,235,225),		// 15
			Color.FromArgb(254,234,224),		// 16
			Color.FromArgb(254,233,224),		// 17
			Color.FromArgb(254,233,223),		// 18
			Color.FromArgb(254,232,222),		// 19
			Color.FromArgb(254,231,221),		// 20
			Color.FromArgb(254,231,220),		// 21
			Color.FromArgb(254,230,219),		// 22
			Color.FromArgb(254,229,218),		// 23
			Color.FromArgb(254,229,217),		// 24
			Color.FromArgb(254,228,216),		// 25
			Color.FromArgb(254,227,215),		// 26
			Color.FromArgb(254,227,214),		// 27
			Color.FromArgb(254,226,213),		// 28
			Color.FromArgb(254,225,212),		// 29
			Color.FromArgb(254,225,211),		// 30
			Color.FromArgb(254,224,210),		// 31
			Color.FromArgb(253,223,209),		// 32
			Color.FromArgb(253,222,208),		// 33
			Color.FromArgb(253,221,206),		// 34
			Color.FromArgb(253,220,205),		// 35
			Color.FromArgb(253,219,203),		// 36
			Color.FromArgb(253,218,202),		// 37
			Color.FromArgb(253,216,200),		// 38
			Color.FromArgb(253,215,199),		// 39
			Color.FromArgb(253,214,197),		// 40
			Color.FromArgb(253,213,195),		// 41
			Color.FromArgb(253,212,194),		// 42
			Color.FromArgb(253,211,192),		// 43
			Color.FromArgb(253,209,191),		// 44
			Color.FromArgb(253,208,189),		// 45
			Color.FromArgb(253,207,188),		// 46
			Color.FromArgb(253,206,186),		// 47
			Color.FromArgb(252,205,185),		// 48
			Color.FromArgb(252,204,183),		// 49
			Color.FromArgb(252,202,182),		// 50
			Color.FromArgb(252,201,180),		// 51
			Color.FromArgb(252,200,179),		// 52
			Color.FromArgb(252,199,177),		// 53
			Color.FromArgb(252,198,175),		// 54
			Color.FromArgb(252,197,174),		// 55
			Color.FromArgb(252,195,172),		// 56
			Color.FromArgb(252,194,171),		// 57
			Color.FromArgb(252,193,169),		// 58
			Color.FromArgb(252,192,168),		// 59
			Color.FromArgb(252,191,166),		// 60
			Color.FromArgb(252,190,165),		// 61
			Color.FromArgb(252,189,163),		// 62
			Color.FromArgb(252,187,162),		// 63
			Color.FromArgb(252,186,160),		// 64
			Color.FromArgb(252,185,159),		// 65
			Color.FromArgb(252,184,157),		// 66
			Color.FromArgb(252,182,156),		// 67
			Color.FromArgb(252,181,154),		// 68
			Color.FromArgb(252,180,153),		// 69
			Color.FromArgb(252,178,151),		// 70
			Color.FromArgb(252,177,150),		// 71
			Color.FromArgb(252,176,148),		// 72
			Color.FromArgb(252,175,147),		// 73
			Color.FromArgb(252,173,145),		// 74
			Color.FromArgb(252,172,144),		// 75
			Color.FromArgb(252,171,142),		// 76
			Color.FromArgb(252,169,141),		// 77
			Color.FromArgb(252,168,139),		// 78
			Color.FromArgb(252,167,138),		// 79
			Color.FromArgb(252,166,137),		// 80
			Color.FromArgb(252,164,135),		// 81
			Color.FromArgb(252,163,134),		// 82
			Color.FromArgb(252,162,132),		// 83
			Color.FromArgb(252,160,131),		// 84
			Color.FromArgb(252,159,129),		// 85
			Color.FromArgb(252,158,128),		// 86
			Color.FromArgb(252,157,126),		// 87
			Color.FromArgb(252,155,125),		// 88
			Color.FromArgb(252,154,123),		// 89
			Color.FromArgb(252,153,122),		// 90
			Color.FromArgb(252,151,120),		// 91
			Color.FromArgb(252,150,119),		// 92
			Color.FromArgb(252,149,117),		// 93
			Color.FromArgb(252,148,116),		// 94
			Color.FromArgb(252,146,114),		// 95
			Color.FromArgb(251,145,113),		// 96
			Color.FromArgb(251,144,112),		// 97
			Color.FromArgb(251,143,111),		// 98
			Color.FromArgb(251,141,109),		// 99
			Color.FromArgb(251,140,108),		// 100
			Color.FromArgb(251,139,107),		// 101
			Color.FromArgb(251,138,106),		// 102
			Color.FromArgb(251,136,104),		// 103
			Color.FromArgb(251,135,103),		// 104
			Color.FromArgb(251,134,102),		// 105
			Color.FromArgb(251,132,100),		// 106
			Color.FromArgb(251,131,99),		// 107
			Color.FromArgb(251,130,98),		// 108
			Color.FromArgb(251,129,97),		// 109
			Color.FromArgb(251,127,95),		// 110
			Color.FromArgb(251,126,94),		// 111
			Color.FromArgb(251,125,93),		// 112
			Color.FromArgb(251,124,92),		// 113
			Color.FromArgb(251,122,90),		// 114
			Color.FromArgb(251,121,89),		// 115
			Color.FromArgb(251,120,88),		// 116
			Color.FromArgb(251,119,87),		// 117
			Color.FromArgb(251,117,85),		// 118
			Color.FromArgb(251,116,84),		// 119
			Color.FromArgb(251,115,83),		// 120
			Color.FromArgb(251,114,82),		// 121
			Color.FromArgb(251,112,80),		// 122
			Color.FromArgb(251,111,79),		// 123
			Color.FromArgb(251,110,78),		// 124
			Color.FromArgb(251,109,77),		// 125
			Color.FromArgb(251,107,75),		// 126
			Color.FromArgb(251,106,74),		// 127
			Color.FromArgb(250,105,73),		// 128
			Color.FromArgb(250,103,72),		// 129
			Color.FromArgb(250,102,71),		// 130
			Color.FromArgb(249,100,70),		// 131
			Color.FromArgb(249,99,69),		// 132
			Color.FromArgb(248,97,68),		// 133
			Color.FromArgb(248,96,67),		// 134
			Color.FromArgb(248,94,66),		// 135
			Color.FromArgb(247,93,66),		// 136
			Color.FromArgb(247,91,65),		// 137
			Color.FromArgb(247,90,64),		// 138
			Color.FromArgb(246,89,63),		// 139
			Color.FromArgb(246,87,62),		// 140
			Color.FromArgb(245,86,61),		// 141
			Color.FromArgb(245,84,60),		// 142
			Color.FromArgb(245,83,59),		// 143
			Color.FromArgb(244,81,58),		// 144
			Color.FromArgb(244,80,57),		// 145
			Color.FromArgb(244,78,56),		// 146
			Color.FromArgb(243,77,55),		// 147
			Color.FromArgb(243,75,54),		// 148
			Color.FromArgb(242,74,53),		// 149
			Color.FromArgb(242,72,52),		// 150
			Color.FromArgb(242,71,51),		// 151
			Color.FromArgb(241,69,50),		// 152
			Color.FromArgb(241,68,50),		// 153
			Color.FromArgb(241,66,49),		// 154
			Color.FromArgb(240,65,48),		// 155
			Color.FromArgb(240,63,47),		// 156
			Color.FromArgb(239,62,46),		// 157
			Color.FromArgb(239,61,45),		// 158
			Color.FromArgb(239,59,44),		// 159
			Color.FromArgb(238,58,43),		// 160
			Color.FromArgb(237,57,43),		// 161
			Color.FromArgb(236,56,42),		// 162
			Color.FromArgb(234,55,42),		// 163
			Color.FromArgb(233,53,41),		// 164
			Color.FromArgb(232,52,41),		// 165
			Color.FromArgb(231,51,40),		// 166
			Color.FromArgb(230,50,40),		// 167
			Color.FromArgb(229,49,39),		// 168
			Color.FromArgb(228,48,39),		// 169
			Color.FromArgb(227,47,39),		// 170
			Color.FromArgb(225,46,38),		// 171
			Color.FromArgb(224,45,38),		// 172
			Color.FromArgb(223,44,37),		// 173
			Color.FromArgb(222,42,37),		// 174
			Color.FromArgb(221,41,36),		// 175
			Color.FromArgb(220,40,36),		// 176
			Color.FromArgb(219,39,35),		// 177
			Color.FromArgb(217,38,35),		// 178
			Color.FromArgb(216,37,34),		// 179
			Color.FromArgb(215,36,34),		// 180
			Color.FromArgb(214,35,33),		// 181
			Color.FromArgb(213,34,33),		// 182
			Color.FromArgb(212,33,32),		// 183
			Color.FromArgb(211,31,32),		// 184
			Color.FromArgb(210,30,31),		// 185
			Color.FromArgb(208,29,31),		// 186
			Color.FromArgb(207,28,31),		// 187
			Color.FromArgb(206,27,30),		// 188
			Color.FromArgb(205,26,30),		// 189
			Color.FromArgb(204,25,29),		// 190
			Color.FromArgb(203,24,29),		// 191
			Color.FromArgb(202,23,28),		// 192
			Color.FromArgb(200,23,28),		// 193
			Color.FromArgb(199,23,28),		// 194
			Color.FromArgb(198,22,28),		// 195
			Color.FromArgb(197,22,27),		// 196
			Color.FromArgb(196,22,27),		// 197
			Color.FromArgb(194,22,27),		// 198
			Color.FromArgb(193,21,27),		// 199
			Color.FromArgb(192,21,26),		// 200
			Color.FromArgb(191,21,26),		// 201
			Color.FromArgb(190,20,26),		// 202
			Color.FromArgb(188,20,26),		// 203
			Color.FromArgb(187,20,25),		// 204
			Color.FromArgb(186,20,25),		// 205
			Color.FromArgb(185,19,25),		// 206
			Color.FromArgb(184,19,25),		// 207
			Color.FromArgb(183,19,24),		// 208
			Color.FromArgb(181,18,24),		// 209
			Color.FromArgb(180,18,24),		// 210
			Color.FromArgb(179,18,24),		// 211
			Color.FromArgb(178,18,23),		// 212
			Color.FromArgb(177,17,23),		// 213
			Color.FromArgb(175,17,23),		// 214
			Color.FromArgb(174,17,23),		// 215
			Color.FromArgb(173,17,22),		// 216
			Color.FromArgb(172,16,22),		// 217
			Color.FromArgb(171,16,22),		// 218
			Color.FromArgb(169,16,22),		// 219
			Color.FromArgb(168,15,21),		// 220
			Color.FromArgb(167,15,21),		// 221
			Color.FromArgb(166,15,21),		// 222
			Color.FromArgb(165,15,21),		// 223
			Color.FromArgb(163,14,20),		// 224
			Color.FromArgb(161,14,20),		// 225
			Color.FromArgb(159,13,20),		// 226
			Color.FromArgb(157,13,20),		// 227
			Color.FromArgb(155,12,19),		// 228
			Color.FromArgb(153,12,19),		// 229
			Color.FromArgb(151,11,19),		// 230
			Color.FromArgb(149,11,19),		// 231
			Color.FromArgb(147,10,18),		// 232
			Color.FromArgb(145,10,18),		// 233
			Color.FromArgb(143,9,18),		// 234
			Color.FromArgb(141,9,18),		// 235
			Color.FromArgb(139,8,17),		// 236
			Color.FromArgb(138,8,17),		// 237
			Color.FromArgb(136,8,17),		// 238
			Color.FromArgb(134,7,17),		// 239
			Color.FromArgb(132,7,16),		// 240
			Color.FromArgb(130,6,16),		// 241
			Color.FromArgb(128,6,16),		// 242
			Color.FromArgb(126,5,16),		// 243
			Color.FromArgb(124,5,15),		// 244
			Color.FromArgb(122,4,15),		// 245
			Color.FromArgb(120,4,15),		// 246
			Color.FromArgb(118,3,15),		// 247
			Color.FromArgb(116,3,14),		// 248
			Color.FromArgb(114,2,14),		// 249
			Color.FromArgb(112,2,14),		// 250
			Color.FromArgb(110,1,14),		// 251
			Color.FromArgb(108,1,13),		// 252
			Color.FromArgb(106,0,13),		// 253
			Color.FromArgb(104,0,13),		// 254
			Color.FromArgb(103,0,12),		// 255
		};
	}
}
