﻿using System.Drawing;
namespace SSTools.ColorMap.Diverging
{
	/// <summary>
	/// Brbgカラーマップ
	/// </summary>
	public class BrbgColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BrbgColorMap()
		{
			colorMap = brbg_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] brbg_map_ =
		{
			Color.FromArgb(84,48,5),		// 0
			Color.FromArgb(86,49,5),		// 1
			Color.FromArgb(88,50,5),		// 2
			Color.FromArgb(90,51,5),		// 3
			Color.FromArgb(92,53,5),		// 4
			Color.FromArgb(94,54,5),		// 5
			Color.FromArgb(97,55,6),		// 6
			Color.FromArgb(99,57,6),		// 7
			Color.FromArgb(101,58,6),		// 8
			Color.FromArgb(103,59,6),		// 9
			Color.FromArgb(105,60,6),		// 10
			Color.FromArgb(108,62,7),		// 11
			Color.FromArgb(110,63,7),		// 12
			Color.FromArgb(112,64,7),		// 13
			Color.FromArgb(114,66,7),		// 14
			Color.FromArgb(116,67,7),		// 15
			Color.FromArgb(119,68,8),		// 16
			Color.FromArgb(121,69,8),		// 17
			Color.FromArgb(123,71,8),		// 18
			Color.FromArgb(125,72,8),		// 19
			Color.FromArgb(127,73,8),		// 20
			Color.FromArgb(130,75,9),		// 21
			Color.FromArgb(132,76,9),		// 22
			Color.FromArgb(134,77,9),		// 23
			Color.FromArgb(136,79,9),		// 24
			Color.FromArgb(138,80,9),		// 25
			Color.FromArgb(141,81,10),		// 26
			Color.FromArgb(143,83,12),		// 27
			Color.FromArgb(145,85,13),		// 28
			Color.FromArgb(147,87,14),		// 29
			Color.FromArgb(149,89,16),		// 30
			Color.FromArgb(151,91,17),		// 31
			Color.FromArgb(153,93,18),		// 32
			Color.FromArgb(155,95,20),		// 33
			Color.FromArgb(157,97,21),		// 34
			Color.FromArgb(159,98,23),		// 35
			Color.FromArgb(161,100,24),		// 36
			Color.FromArgb(163,102,25),		// 37
			Color.FromArgb(165,104,27),		// 38
			Color.FromArgb(167,106,28),		// 39
			Color.FromArgb(169,108,29),		// 40
			Color.FromArgb(171,110,31),		// 41
			Color.FromArgb(173,112,32),		// 42
			Color.FromArgb(175,113,34),		// 43
			Color.FromArgb(177,115,35),		// 44
			Color.FromArgb(179,117,36),		// 45
			Color.FromArgb(181,119,38),		// 46
			Color.FromArgb(183,121,39),		// 47
			Color.FromArgb(185,123,40),		// 48
			Color.FromArgb(187,125,42),		// 49
			Color.FromArgb(189,127,43),		// 50
			Color.FromArgb(191,129,45),		// 51
			Color.FromArgb(192,131,48),		// 52
			Color.FromArgb(193,134,51),		// 53
			Color.FromArgb(194,136,54),		// 54
			Color.FromArgb(196,139,57),		// 55
			Color.FromArgb(197,141,60),		// 56
			Color.FromArgb(198,144,63),		// 57
			Color.FromArgb(199,146,66),		// 58
			Color.FromArgb(201,149,70),		// 59
			Color.FromArgb(202,151,73),		// 60
			Color.FromArgb(203,154,76),		// 61
			Color.FromArgb(204,157,79),		// 62
			Color.FromArgb(206,159,82),		// 63
			Color.FromArgb(207,162,85),		// 64
			Color.FromArgb(208,164,88),		// 65
			Color.FromArgb(209,167,92),		// 66
			Color.FromArgb(211,169,95),		// 67
			Color.FromArgb(212,172,98),		// 68
			Color.FromArgb(213,174,101),		// 69
			Color.FromArgb(214,177,104),		// 70
			Color.FromArgb(216,179,107),		// 71
			Color.FromArgb(217,182,110),		// 72
			Color.FromArgb(218,185,114),		// 73
			Color.FromArgb(219,187,117),		// 74
			Color.FromArgb(221,190,120),		// 75
			Color.FromArgb(222,192,123),		// 76
			Color.FromArgb(223,194,126),		// 77
			Color.FromArgb(224,196,129),		// 78
			Color.FromArgb(225,197,131),		// 79
			Color.FromArgb(226,199,134),		// 80
			Color.FromArgb(227,200,137),		// 81
			Color.FromArgb(227,202,140),		// 82
			Color.FromArgb(228,203,142),		// 83
			Color.FromArgb(229,205,145),		// 84
			Color.FromArgb(230,206,148),		// 85
			Color.FromArgb(231,208,151),		// 86
			Color.FromArgb(232,209,153),		// 87
			Color.FromArgb(233,211,156),		// 88
			Color.FromArgb(234,212,159),		// 89
			Color.FromArgb(235,214,162),		// 90
			Color.FromArgb(236,215,164),		// 91
			Color.FromArgb(236,217,167),		// 92
			Color.FromArgb(237,218,170),		// 93
			Color.FromArgb(238,220,173),		// 94
			Color.FromArgb(239,221,175),		// 95
			Color.FromArgb(240,223,178),		// 96
			Color.FromArgb(241,224,181),		// 97
			Color.FromArgb(242,226,184),		// 98
			Color.FromArgb(243,227,186),		// 99
			Color.FromArgb(244,229,189),		// 100
			Color.FromArgb(245,230,192),		// 101
			Color.FromArgb(246,232,195),		// 102
			Color.FromArgb(245,232,196),		// 103
			Color.FromArgb(245,233,198),		// 104
			Color.FromArgb(245,233,200),		// 105
			Color.FromArgb(245,234,202),		// 106
			Color.FromArgb(245,234,204),		// 107
			Color.FromArgb(245,235,206),		// 108
			Color.FromArgb(245,235,208),		// 109
			Color.FromArgb(245,236,210),		// 110
			Color.FromArgb(245,236,212),		// 111
			Color.FromArgb(245,237,214),		// 112
			Color.FromArgb(245,237,216),		// 113
			Color.FromArgb(245,238,218),		// 114
			Color.FromArgb(245,238,220),		// 115
			Color.FromArgb(245,239,222),		// 116
			Color.FromArgb(245,239,224),		// 117
			Color.FromArgb(245,240,226),		// 118
			Color.FromArgb(245,240,228),		// 119
			Color.FromArgb(245,241,230),		// 120
			Color.FromArgb(245,241,232),		// 121
			Color.FromArgb(245,242,234),		// 122
			Color.FromArgb(245,242,236),		// 123
			Color.FromArgb(245,243,238),		// 124
			Color.FromArgb(245,243,240),		// 125
			Color.FromArgb(245,244,242),		// 126
			Color.FromArgb(245,244,244),		// 127
			Color.FromArgb(244,244,244),		// 128
			Color.FromArgb(242,244,244),		// 129
			Color.FromArgb(240,243,243),		// 130
			Color.FromArgb(238,243,242),		// 131
			Color.FromArgb(236,243,242),		// 132
			Color.FromArgb(235,242,241),		// 133
			Color.FromArgb(233,242,240),		// 134
			Color.FromArgb(231,241,240),		// 135
			Color.FromArgb(229,241,239),		// 136
			Color.FromArgb(227,240,239),		// 137
			Color.FromArgb(226,240,238),		// 138
			Color.FromArgb(224,240,237),		// 139
			Color.FromArgb(222,239,237),		// 140
			Color.FromArgb(220,239,236),		// 141
			Color.FromArgb(218,238,235),		// 142
			Color.FromArgb(217,238,235),		// 143
			Color.FromArgb(215,237,234),		// 144
			Color.FromArgb(213,237,234),		// 145
			Color.FromArgb(211,237,233),		// 146
			Color.FromArgb(209,236,232),		// 147
			Color.FromArgb(208,236,232),		// 148
			Color.FromArgb(206,235,231),		// 149
			Color.FromArgb(204,235,230),		// 150
			Color.FromArgb(202,234,230),		// 151
			Color.FromArgb(200,234,229),		// 152
			Color.FromArgb(199,234,229),		// 153
			Color.FromArgb(196,232,227),		// 154
			Color.FromArgb(193,231,226),		// 155
			Color.FromArgb(190,230,224),		// 156
			Color.FromArgb(187,229,223),		// 157
			Color.FromArgb(185,228,221),		// 158
			Color.FromArgb(182,227,220),		// 159
			Color.FromArgb(179,226,219),		// 160
			Color.FromArgb(176,224,217),		// 161
			Color.FromArgb(173,223,216),		// 162
			Color.FromArgb(171,222,214),		// 163
			Color.FromArgb(168,221,213),		// 164
			Color.FromArgb(165,220,212),		// 165
			Color.FromArgb(162,219,210),		// 166
			Color.FromArgb(160,218,209),		// 167
			Color.FromArgb(157,216,207),		// 168
			Color.FromArgb(154,215,206),		// 169
			Color.FromArgb(151,214,205),		// 170
			Color.FromArgb(148,213,203),		// 171
			Color.FromArgb(146,212,202),		// 172
			Color.FromArgb(143,211,200),		// 173
			Color.FromArgb(140,210,199),		// 174
			Color.FromArgb(137,208,197),		// 175
			Color.FromArgb(134,207,196),		// 176
			Color.FromArgb(132,206,195),		// 177
			Color.FromArgb(129,205,193),		// 178
			Color.FromArgb(126,203,192),		// 179
			Color.FromArgb(123,201,190),		// 180
			Color.FromArgb(120,199,188),		// 181
			Color.FromArgb(117,197,186),		// 182
			Color.FromArgb(114,195,184),		// 183
			Color.FromArgb(111,193,182),		// 184
			Color.FromArgb(108,191,180),		// 185
			Color.FromArgb(105,189,178),		// 186
			Color.FromArgb(103,187,176),		// 187
			Color.FromArgb(100,184,174),		// 188
			Color.FromArgb(97,182,172),		// 189
			Color.FromArgb(94,180,170),		// 190
			Color.FromArgb(91,178,168),		// 191
			Color.FromArgb(88,176,166),		// 192
			Color.FromArgb(85,174,164),		// 193
			Color.FromArgb(82,172,162),		// 194
			Color.FromArgb(79,170,160),		// 195
			Color.FromArgb(76,167,158),		// 196
			Color.FromArgb(73,165,156),		// 197
			Color.FromArgb(70,163,154),		// 198
			Color.FromArgb(67,161,152),		// 199
			Color.FromArgb(64,159,150),		// 200
			Color.FromArgb(61,157,148),		// 201
			Color.FromArgb(58,155,146),		// 202
			Color.FromArgb(55,153,144),		// 203
			Color.FromArgb(53,151,143),		// 204
			Color.FromArgb(50,149,141),		// 205
			Color.FromArgb(48,147,139),		// 206
			Color.FromArgb(46,145,137),		// 207
			Color.FromArgb(44,143,135),		// 208
			Color.FromArgb(42,141,133),		// 209
			Color.FromArgb(40,139,131),		// 210
			Color.FromArgb(38,137,129),		// 211
			Color.FromArgb(36,135,127),		// 212
			Color.FromArgb(34,133,125),		// 213
			Color.FromArgb(32,131,123),		// 214
			Color.FromArgb(30,129,121),		// 215
			Color.FromArgb(28,127,119),		// 216
			Color.FromArgb(26,126,118),		// 217
			Color.FromArgb(24,124,116),		// 218
			Color.FromArgb(22,122,114),		// 219
			Color.FromArgb(20,120,112),		// 220
			Color.FromArgb(18,118,110),		// 221
			Color.FromArgb(16,116,108),		// 222
			Color.FromArgb(14,114,106),		// 223
			Color.FromArgb(12,112,104),		// 224
			Color.FromArgb(10,110,102),		// 225
			Color.FromArgb(8,108,100),		// 226
			Color.FromArgb(6,106,98),		// 227
			Color.FromArgb(4,104,96),		// 228
			Color.FromArgb(2,102,94),		// 229
			Color.FromArgb(0,101,93),		// 230
			Color.FromArgb(0,99,91),		// 231
			Color.FromArgb(0,97,89),		// 232
			Color.FromArgb(0,96,87),		// 233
			Color.FromArgb(0,94,85),		// 234
			Color.FromArgb(0,92,84),		// 235
			Color.FromArgb(0,91,82),		// 236
			Color.FromArgb(0,89,80),		// 237
			Color.FromArgb(0,88,78),		// 238
			Color.FromArgb(0,86,76),		// 239
			Color.FromArgb(0,84,75),		// 240
			Color.FromArgb(0,83,73),		// 241
			Color.FromArgb(0,81,71),		// 242
			Color.FromArgb(0,79,69),		// 243
			Color.FromArgb(0,78,67),		// 244
			Color.FromArgb(0,76,66),		// 245
			Color.FromArgb(0,74,64),		// 246
			Color.FromArgb(0,73,62),		// 247
			Color.FromArgb(0,71,60),		// 248
			Color.FromArgb(0,69,58),		// 249
			Color.FromArgb(0,68,57),		// 250
			Color.FromArgb(0,66,55),		// 251
			Color.FromArgb(0,64,53),		// 252
			Color.FromArgb(0,63,51),		// 253
			Color.FromArgb(0,61,49),		// 254
			Color.FromArgb(0,60,48),		// 255
		};
	}
}
